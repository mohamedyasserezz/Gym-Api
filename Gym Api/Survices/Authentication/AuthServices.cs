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
using Gym_Api.DTO.ResetPassword;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace Gym_Api.Survices.Authentication
{
    public class AuthServices(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtProvider jwtProvider,
        ILogger<AuthResponse> logger,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext context) : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ILogger<AuthResponse> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ApplicationDbContext _context = context;
        private readonly int _refreshTokenExpiryDays = 14;


        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var emailExist = await _userManager.FindByEmailAsync(request.Email);

            if (emailExist is not null)
                return Result.Failer(UserErrors.DuplicatedEmail);

            var newUser = new ApplicationUser
            {
                FullName = $"{request.FirstName} {request.LastName}",
                UserName = request.UserName,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {

                
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation("Confirmation code: {code}", code);

                //TODO send email

                await SendConfirmationEmailAsync(newUser, code);

                return Result.Success();
            }

            var error = result.Errors.First();

            return Result.Failer(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

        }
        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {

            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failer<AuthResponse>(UserErrors.InvalidCredentials);


            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

            if (result.Succeeded)
            {

                var roles = await _userManager.GetRolesAsync(user);

                var (token, expiresIn) = _jwtProvider.GenerateToken(user, roles);

                var refreshToken = GenerateRefreshToken();
                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),

                });
                await _userManager.UpdateAsync(user);
                var response = new AuthResponse(
                    user.Id,
                    user.Email,
                    user.FullName,
                    token,
                    expiresIn * 60,
                    refreshToken,
                    DateTime.UtcNow.AddDays(_refreshTokenExpiryDays));

                return Result.Success(response);
            }
            var isLokout = result.IsLockedOut;



            return Result.Failer<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed :
                result.IsLockedOut ?
                UserErrors.LockedUser :
                UserErrors.InvalidCredentials);
        }
        public async Task<Result<AuthResponse?>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId is null)
                return Result.Failer<AuthResponse?>(UserErrors.InvalidCredentials);

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failer<AuthResponse?>(UserErrors.InvalidCredentials);


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(U => U.Token == refreshToken && U.IsActive);

            if (userRefreshToken is null) return Result.Failer<AuthResponse?>(UserErrors.InvalidCredentials);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var roles = await _userManager.GetRolesAsync(user);

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, roles);

            var NewRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = NewRefreshToken,
                ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),

            });
            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(
                user.Id,
                user.Email,
                user.FullName,
                newToken,
                expiresIn * 60,
                NewRefreshToken,
                DateTime.UtcNow.AddDays(_refreshTokenExpiryDays));

            return Result.Success(response)!;
        }
        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failer(UserErrors.InvalidCredentials);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) return Result.Failer(UserErrors.InvalidCredentials);
            var userRefreshToken = user.RefreshTokens.SingleOrDefault(R => R.Token == refreshToken && R.IsActive);

            if (userRefreshToken is null) return Result.Failer(UserErrors.InvalidCredentials);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            return Result.Success();

        }

        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failer(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failer(UserErrors.DuplicatedEmail);

            var code = request.Code;
            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "Error while confirming email");
                return Result.Failer(UserErrors.InvalidCode);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, DefaultRoles.User);
                return Result.Success();


            }

            var error = result.Errors.First();

            return Result.Failer(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> ResendConfirmationEmail(ResendConfirmationEmailRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();

            if (user.EmailConfirmed)
                return Result.Failer(UserErrors.DuplicatedEmail);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            _logger.LogInformation("Confirmation code: {code}", code);

            await SendConfirmationEmailAsync(user, code);

            return Result.Success();
        }
        public async Task<Result> SendResetPasswordCodeAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failer(UserErrors.EmailNotConfirmed);

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Reset code: {code}", code);

            await SendResetPasswordEmailAsync(user, code);

            return Result.Success();
        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user || !user.EmailConfirmed)
                return Result.Failer(UserErrors.InvalidCode);

            var code = request.Code;
            IdentityResult result;
            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

                result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "Error while resetting password");
                result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());

            }


            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failer(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
        }

        public static string GenerateRefreshToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        private async Task SendConfirmationEmailAsync(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody(
                "EmailConfirmation",
                new Dictionary<string, string>
                {
                        { "{{name}}", user.FullName },
                        { "{{action_url}}", $"{origin}/auth/confirm-email?userId={user.Id}&code={code}" }
                });

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Confirm your email", emailBody));

            await Task.CompletedTask;
        }

        private async Task SendResetPasswordEmailAsync(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody(
                "ForgetPassword",
                new Dictionary<string, string>
                {
                        { "{{name}}", user.FullName },
                        { "{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }
                });

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "Change Password", emailBody));

            await Task.CompletedTask;
        }

       
    }
}
