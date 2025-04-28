using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Assignment
	{
		[Key]
		public int Assignment_ID { get; set; }
		public string Day { get; set; } 
		public bool IsCompleted { get; set; } = false;

		[ForeignKey("User")]
		public int User_ID { get; set; }
		public User User { get; set; }

		[ForeignKey("Coach")]
		public int Coach_ID { get; set; }
		public Coach Coach { get; set; }

		[ForeignKey("Exercise")]
		public int Exercise_ID { get; set; }
		public Exercise Exercise { get; set; }
	}
}
