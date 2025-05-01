using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Order
	{
		[Key]
		public int Order_id { get; set; }
		public double Total_Price { get; set; }
		public DateTime Order_Date { get; set; }
		public string Order_Status { get; set; } = "Pending"; // "Pending", "Completed", "Rejected"

		public bool IsPaid { get; set; } = false; // هل تم الدفع؟
		public string? PaymentProof { get; set; } // صورة أو رقم التحويل
		public string User_ID { get; set; }
		public User User { get; set; }
		public ICollection<OrderItem> OrderItems = new List <OrderItem>();

	}
}
