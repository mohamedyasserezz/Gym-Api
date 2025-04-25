using System.ComponentModel.DataAnnotations;

namespace Gym_Api.DTO
{
	public class Addnewcategory
	{
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public IFormFile CategoryImage { get; set; }
    }
}
