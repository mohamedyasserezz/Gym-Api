namespace Gym_Api.DTO
{
	public class CreateAssignmentDto
	{
		public int CoachId { get; set; }
		public int UserId { get; set; }
		public int ExerciseId { get; set; }
		public string Day { get; set; }
		public string? Notes { get; set; }
	}
}
