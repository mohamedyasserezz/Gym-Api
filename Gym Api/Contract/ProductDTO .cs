namespace Gym_Api.Contract
{
	public class ProductDTO
	{
		public string Product_Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public double Discount { get; set; }
		public int Stock_Quantity { get; set; }
		public string Image_URL { get; set; }
	}
}
