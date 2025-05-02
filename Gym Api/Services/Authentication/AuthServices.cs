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

        public Task<Result> AssignOtpForPassword(ResetPasswordRequest reset)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AuthResponse>> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ResetPasswordAsync(AssignNewPassword request)
        {
            throw new NotImplementedException();
        }

        public Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Result> SendResetPasswordOtpAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
