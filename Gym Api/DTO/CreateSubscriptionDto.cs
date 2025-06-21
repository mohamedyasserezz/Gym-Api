using System.ComponentModel.DataAnnotations;

namespace Gym_Api.DTO
{
	public class CreateSubscriptionDto
	{
		[Required]
		public string User_ID { get; set; }
		[Required]
		public string Coach_ID { get; set; }
		[Required]
		public string SubscriptionType { get; set; } // "شهر" - "3 شهور" - "6 شهور"
		[Required]
		public IFormFile PaymentProof { get; set; }
		public string? Notes { get; set; }

	}
}
