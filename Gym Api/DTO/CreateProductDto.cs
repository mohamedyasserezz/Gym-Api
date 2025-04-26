using System.ComponentModel.DataAnnotations;

namespace Gym_Api.DTO
{
	public class CreateProductDto
	{
		public string Product_Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public double Discount { get; set; }
		public int Stock_Quantity { get; set; }
		[Required]
		public IFormFile ProductImage { get; set; }
	}
}
