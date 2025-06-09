namespace Gym_Api.DTO
{
	public class CreateAssignmentDto
	{
		public string Day { get; set; }
		public string? Notes { get; set; }
		public List<int> ExerciseIds { get; set; }
		

	}
}
