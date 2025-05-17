namespace Gym_Api.DTO
{
	public class CreateAssignmentDto
	{
		public string CoachId { get; set; }
		public string UserId { get; set; }
		public int ExerciseId { get; set; }
		public string Day { get; set; }
		public string? Notes { get; set; }

	}
}
