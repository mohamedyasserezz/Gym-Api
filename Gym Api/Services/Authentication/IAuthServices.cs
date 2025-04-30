using Gym_Api.Common;
using Gym_Api.DTO.Authentication;
using Gym_Api.DTO.Authentication.ConfirmEmail;
using Gym_Api.DTO.Authentication.Register;
using Gym_Api.DTO.Authentication.ResendConfirmationEmail;
using Gym_Api.DTO.ResetPassword;

namespace Gym_Api.Survices.Authentication
{
    public interface IAuthServices
    {
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse?>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmail(ResendConfirmationEmailRequest request);
        Task<Result> SendResetPasswordCodeAsync(string email);
        Task<Result> ResetPasswordAsync(ResetPasswordRequest request);

    }
}
