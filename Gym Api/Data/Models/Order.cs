using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Order
	{
		[Key]
		public int Order_id { get; set; }
		public DateTime Order_Date { get; set; } = DateTime.UtcNow;
		public string Order_Status { get; set; } = "Pending"; // "Pending", "Accepted", "Rejected"
		public bool IsPaid { get; set; } = false; // هل تم الدفع؟
		public string? PaymentProof { get; set; } // صورة أو رقم التحويل
		public double TotalPrice { get; set; }
		public string RecipientName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public int PhoneNumber { get; set; }

		public string User_ID { get; set; }
		public User User { get; set; }
		public ICollection<OrderItem> OrderItems = new List <OrderItem>();

	}
}
