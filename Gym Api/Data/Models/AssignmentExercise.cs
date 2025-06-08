using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Api.Data.Models
{
	public class AssignmentExercise
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Exercise")]
		public int Exercise_ID { get; set; }
		public Exercise Exercise { get; set; }

		[ForeignKey("Assignment")]
		public int AssignmentId { get; set; }
		public Assignment Assignment { get; set; }
	}
}
