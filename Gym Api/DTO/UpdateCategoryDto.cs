namespace Gym_Api.DTO
{
	public class UpdateCategoryDto
	{
		public string? CategoryName { get; set; }
		public IFormFile? CategoryImage { get; set; }
	}
}
