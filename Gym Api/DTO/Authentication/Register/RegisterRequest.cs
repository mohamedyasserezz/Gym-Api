namespace Gym_Api.DTO.Authentication.Register
{
    public record RegisterRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password
    );
}
