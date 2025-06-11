using System.ComponentModel.DataAnnotations;

namespace Gym_Api.DTO
{
	public class CreateAssignmentDto
	{
		public DateTime Day { get; set; }
		public string? Notes { get; set; }
		[Required]
		public List<int> ExerciseIds { get; set; }
		

	}
}
