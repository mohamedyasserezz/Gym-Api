namespace Gym_Api.DTO
{
	public class UpdateProductDto
	{
		public string Product_Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public double Discount { get; set; }
		public int Stock_Quantity { get; set; }
		public IFormFile? ProductImage { get; set; }
	}
}
