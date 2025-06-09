namespace Gym_Api.DTO
{
	public class BulkCreateAssignmentDto
	{
		public string UserId { get; set; }
		public string CoachId { get; set; }
		public List<CreateAssignmentDto> Assignments { get; set; }
	}
}
