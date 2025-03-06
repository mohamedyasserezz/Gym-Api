using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class OrderItem
	{
		[Key]
		public int OrderItem_ID { get; set; }

		public int Quantity { get; set; }
		public double TotalPrice { get; set; }

		[ForeignKey("Order")]
		public int Order_ID { get; set; }
		public Order Order { get; set; }

		[ForeignKey("Product")]
		public int Product_ID { get; set; }
		public Product Product { get; set; }
	}
}
