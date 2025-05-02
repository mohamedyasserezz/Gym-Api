using Microsoft.AspNetCore.Identity;

namespace Gym_Api.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? Image { get; set; } = default!;
        public UserType UserType { get; set; }
        public bool IsForgetPasswordOtpConfirmed { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
