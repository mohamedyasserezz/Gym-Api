namespace Gym_Api.DTO
{
	public class AssignmentViewDto
	{
		public int Assignment_ID { get; set; }
		public string Day { get; set; }
		public string? Notes { get; set; }
		public bool IsCompleted { get; set; }
		public List<ExerciseDto> Exercises { get; set; }
	}
}
