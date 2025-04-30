namespace Gym_Api.DTO.Authentication.RefreshToken
{
    public record RefreshTokenRequest(
        string Token,
        string RefreshToken
        );
}
