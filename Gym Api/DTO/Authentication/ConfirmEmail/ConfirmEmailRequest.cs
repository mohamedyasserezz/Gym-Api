namespace Gym_Api.DTO.Authentication.ConfirmEmail
{
    public record ConfirmEmailRequest(
    string Email,
    string Otp
);
}
