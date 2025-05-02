namespace Gym_Api.DTO.Authentication.ResetPassword
{
    public record ResetPasswordRequest(
    string Email,
    string Otp
);
}
