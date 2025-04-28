using System.ComponentModel.DataAnnotations;

namespace Gym_Api.DTO
{
	public class CreateSubscriptionDto
	{
		[Required]
		public int User_ID { get; set; }
		[Required]
		public int Coach_ID { get; set; }
		[Required]
		public string SubscriptionType { get; set; } // "شهر" - "3 شهور" - "6 شهور"
		[Required]
		public IFormFile PaymentProof { get; set; }
	}
}
