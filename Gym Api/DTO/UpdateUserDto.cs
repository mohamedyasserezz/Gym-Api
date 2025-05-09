namespace Gym_Api.DTO
{
	public class UpdateUserDto
	{
		public double Height { get; set; }
		public double Weight { get; set; }
		public DateTime BDate { get; set; }
		public string Gender { get; set; } = default!;
		public string? MedicalConditions { get; set; }
		public string? Allergies { get; set; }
		public string Fitness_Goal { get; set; } = default!;
	}
}
