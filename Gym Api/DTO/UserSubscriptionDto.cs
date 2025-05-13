namespace Gym_Api.DTO
{
	public class UserSubscriptionDto
	{
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public string Image { get; set; }
		public double Height { get; set; }
		public double Weight { get; set; }
		public DateTime BDate { get; set; }
		public string Gender { get; set; } = default!;
		public string? MedicalConditions { get; set; }//هل يعاني من امراض مزمنه
		public string? Allergies { get; set; }//حساسيه من اطعمه معينه
		public string Fitness_Goal { get; set; } = default!;
	}
}
