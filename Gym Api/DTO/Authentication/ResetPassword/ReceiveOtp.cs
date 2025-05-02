namespace Gym_Api.DTO.Authentication.ResetPassword
{
    public record ReceiveOtp(
    string Email,
    string Otp
);
}
