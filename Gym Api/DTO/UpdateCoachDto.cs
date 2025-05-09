namespace Gym_Api.DTO
{
	public class UpdateCoachDto
	{
		public string Specialization { get; set; } = null!;
		public string Portfolio_Link { get; set; } = default!;
		public int Experience_Years { get; set; }
		public string Availability { get; set; } = default!;
		public string Bio { get; set; } = default!;
	}
}
