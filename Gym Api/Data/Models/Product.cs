using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Product
	{
		[Key]
		public int Product_ID { get; set; }
		public string Product_Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public double Discount { get; set; }
		public int Stock_Quantity { get; set; }
		public string Image_URL { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }

	}
}
