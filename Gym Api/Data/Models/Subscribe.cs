using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Subscribe
	{
		[Key]
		public int Subscribe_ID { get; set; }  

		public DateTime StartDate { get; set; } = DateTime.UtcNow;
		public DateTime EndDate { get; set; }
		public bool IsPaid { get; set; } = false;
		public bool IsApproved { get; set; } = false;
		public string? PaymentProof { get; set; }


		[ForeignKey("User")]
		public int User_ID { get; set; }
		public User User { get; set; }

		[ForeignKey("Coach")]
		public int Coach_ID { get; set; }
		public Coach Coach { get; set; }
	}
}
