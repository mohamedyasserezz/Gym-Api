using Gym_Api.Common;
using Gym_Api.DTO.Authentication;
using Gym_Api.DTO.Authentication.CompleteProfile;
using Gym_Api.DTO.Authentication.ConfirmEmail;
using Gym_Api.DTO.Authentication.Register;
using Gym_Api.DTO.Authentication.ResendConfirmationEmail;
using Gym_Api.DTO.Authentication.ResetPassword;

namespace Gym_Api.Survices.Authentication
{
    public interface IAuthServices
    {
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> CompleteCoachRegistration(RegisterCoachRequest request, CancellationToken cancellationToken = default);
		Task<Result<AuthResponse>> CompleteUserRegistration(RegsisterUserRequest request, CancellationToken cancellationToken = default);
		Task<Result<AuthResponse>> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request);
        Task<Result> SendResetPasswordOtpAsync(string email);
        Task<Result> ResetPasswordAsync(AssignNewPassword request);
        Task<Result> AssignOtpForPassword(ResetPasswordRequest reset);


    }
}
