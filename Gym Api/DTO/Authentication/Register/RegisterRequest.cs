namespace Gym_Api.DTO.Authentication.Register
{
    public record RegisterRequest(
    string FullName,
    string UserName,
    string Email,
    IFormFile? Image,
    string Password,
    string UserType
    );
}
