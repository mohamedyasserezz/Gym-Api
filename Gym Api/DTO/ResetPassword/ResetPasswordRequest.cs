namespace Gym_Api.DTO.ResetPassword
{
    public record ResetPasswordRequest(
        string Email,
        string Code,
        string NewPassword
        );
}
