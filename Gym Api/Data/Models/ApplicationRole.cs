using Microsoft.AspNetCore.Identity;

namespace Gym_Api.Data.Models
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}
