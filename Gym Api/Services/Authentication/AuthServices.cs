using Gym_Api.Common;
using Gym_Api.Common.Consts;
using Gym_Api.Common.Consts.Errors;
using Gym_Api.Common.Consts.Helpers;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO.Authentication;
using Gym_Api.DTO.Authentication.ConfirmEmail;
using Gym_Api.DTO.Authentication.Register;
using Gym_Api.DTO.Authentication.ResendConfirmationEmail;
using Gym_Api.DTO.Authentication.ResetPassword;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Gym_Api.Survices.Authentication
{
    public class AuthServices(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtProvider jwtProvider,
        ILogger<AuthResponse> logger,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        IFileService fileService,
        ApplicationDbContext context) : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthResponse> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IFileService _fileService = fileService;
        private readonly ApplicationDbContext _context = context;



        private static readonly Dictionary<string, (string Otp, DateTime Expiry)> _otpStore = new();
        private readonly int _otpExpiryMinutes = 15;
        private readonly int _refreshTokenExpiryDays = 14;

        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);



            if (user.UserType == UserType.Coach)
            {
                var coach = await _context.Coaches
                    .Include(x => x.ApplicationUser)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
                if (coach is null)
                    return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

                if (!coach.IsConfirmedByAdmin)
                    return Result.Failure<AuthResponse>(UserErrors.NotCompletedProfile);


            }
            else if (user.UserType == UserType.User)
            {
                var User = await _context.Users
                    .Include(x => x.ApplicationUser)
                    .FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
                if (User is null)
                    return Result.Failure<AuthResponse>(UserErrors.UserNotFound);
            }
            else if (user.UserType == UserType.Trainee)
            {
                var trainee = await _userManager.FindByIdAsync(user.Id);
                if (trainee is null)
                    return Result.Failure<AuthResponse>(UserErrors.UserNotFound);


            }


            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration
                });

                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(user.Id,
                    user.Email,
                    user.FullName,
                    user.Image,
                    token,
                    expiresIn,
                    refreshToken,
                    refreshTokenExpiration);

                return Result.Success(response);
            }

            var error = result.IsNotAllowed
                ? UserErrors.EmailNotConfirmed
                : result.IsLockedOut
                ? UserErrors.LockedUser
                : UserErrors.InvalidCredentials;

            return Result.Failure<AuthResponse>(error);
        }

        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);
            // TODO: check if user is disabled
            //if (user.IsDisabled)
            //    return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

            if (user.LockoutEnd > DateTime.UtcNow)
                return Result.Failure<AuthResponse>(UserErrors.LockedUser);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var userRoles = await _userManager.GetRolesAsync(user);

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(user.Id,
                    user.Email,
                    user.FullName,
                    user.Image,
                    token,
                    expiresIn,
                    refreshToken,
                    refreshTokenExpiration);

            return Result.Success(response);
        }

        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailIsExists)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var user = new ApplicationUser
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.Email,
            };
            user.UserType = (UserType)Enum.Parse(typeof(UserType), request.UserType);

            if (request.Image is not null)
            {
                user.Image = await _fileService.SaveFileAsync(request.Image, "profiles");
            }
            
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (user.UserType == UserType.Coach)
                {
                    await _userManager.AddToRoleAsync(user, Users.Coach);
                    var patient = new Coach
                    {
                        UserId = user.Id,
                        ApplicationUser = user,
                    };
                    await _context.Coaches.AddAsync(patient);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else if (user.UserType == UserType.User)
                {
                    await _userManager.AddToRoleAsync(user, Users.User);

                    var doctor = new User
                    {
                        UserId = user.Id,
                        ApplicationUser = user,
                    };
                    await _context.Users.AddAsync(doctor);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else if (user.UserType == UserType.Admin)
                {
                    await _userManager.AddToRoleAsync(user, Users.Admin);
                }
                else if (user.UserType == UserType.Trainee)
                {
                    await _userManager.AddToRoleAsync(user, Users.Trainee);
                }
                string otp = GenerateOTP();

                var key = $"EMAIL_CONFIRM_{user.Id}";

                _otpStore[key] = (otp, DateTime.UtcNow.AddMinutes(_otpExpiryMinutes));

                _logger.LogInformation("Confirmation otp: {otp}", otp);

                await SendConfirmationEmail(user, otp);

                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }


        public async Task<Result<AuthResponse>> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidOtp);

            if (user.EmailConfirmed)
                return Result.Failure<AuthResponse>(UserErrors.DuplicatedConfirmation);

            var key = $"EMAIL_CONFIRM_{user.Id}";

            // Check if OTP exists and is valid
            if (!_otpStore.TryGetValue(key, out var otpInfo) ||
                otpInfo.Otp != request.Otp ||
                otpInfo.Expiry < DateTime.UtcNow)
            {
                return Result.Failure<AuthResponse>(UserErrors.InvalidOtp);
            }

            // Remove the OTP from store after usage
            _otpStore.Remove(key);

            // Generate a token for internal use with Identity
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {

                var userRoles = await _userManager.GetRolesAsync(user);
                var (tokenn, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                var response = new AuthResponse(user.Id,
                    user.Email,
                    user.FullName,
                    user.Image,
                    token,
                    expiresIn,
                    refreshToken,
                    refreshTokenExpiration);

                return Result.Success(response);
            }

            var error = result.Errors.First();

            return Result.Failure<AuthResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            string otp = GenerateOTP();

            // Store OTP with expiry time
            var key = $"EMAIL_CONFIRM_{user.Id}";
            _otpStore[key] = (otp, DateTime.UtcNow.AddMinutes(_otpExpiryMinutes));

            _logger.LogInformation("New confirmation OTP for {email}: {otp}", user.Email, otp);

            await SendConfirmationEmail(user, otp);

            return Result.Success();
        }

        public async Task<Result> SendResetPasswordOtpAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            string otp = GenerateOTP();

            // Store OTP with expiry time
            var key = $"PWD_RESET_{user.Id}";
            _otpStore[key] = (otp, DateTime.UtcNow.AddMinutes(_otpExpiryMinutes));

            _logger.LogInformation("Password reset OTP for {email}: {otp}", user.Email, otp);

            await SendResetPasswordEmail(user, otp);

            return Result.Success();
        }

        public async Task<Result> AssignOtpForPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.InvalidOtp);

            var key = $"PWD_RESET_{user.Id}";

            _logger.LogInformation("Password reset OTP for {email}: {otp}", user.Email, request.Otp);
            // Check if OTP exists and is valid
            if (!_otpStore.TryGetValue(key, out var otpInfo) ||
                otpInfo.Otp != request.Otp ||
                otpInfo.Expiry < DateTime.UtcNow)
            {
                return Result.Failure(UserErrors.InvalidOtp);
            }

            // Remove the OTP from store after usage
            _otpStore.Remove(key);

            user.IsForgetPasswordOtpConfirmed = true;

            await _userManager.UpdateAsync(user);

            return Result.Success();

        }
        public async Task<Result> ResetPasswordAsync(AssignNewPassword request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !user.EmailConfirmed || !user.IsForgetPasswordOtpConfirmed)
                return Result.Failure(UserErrors.InvalidOtp);



            // Generate a token for internal use with Identity
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (result.Succeeded)
            {
                user.IsForgetPasswordOtpConfirmed = false;
                await _userManager.UpdateAsync(user);
                return Result.Success();
            }


            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
        }
        private async Task SendConfirmationEmail(ApplicationUser user, string otp)
        {
            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                    { "{{name}}", user.FullName },
                    { "{{otp_code}}", otp }
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Gym Mate Health: Email Confirmation", emailBody));

            await Task.CompletedTask;
        }
        private async Task SendResetPasswordEmail(ApplicationUser user, string otp)
        {
            var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                    { "{{name}}", user.FullName },
                    { "{{otp_code}}", otp }
                }
            );

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ Gym Mate Health: Password Reset", emailBody));

            await Task.CompletedTask;
        }
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        private static string GenerateOTP(int length = 4)
        {
            // Generate a numeric OTP of specified length
            Random random = new Random();
            return new string(Enumerable.Repeat(0, length)
                .Select(_ => random.Next(0, 10).ToString()[0])
                .ToArray());
        }

        
    }
}
