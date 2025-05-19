using Gym_Api.Data.Models;

namespace Gym_Api.DTO
{
	public class UserList
	{
		public string UserId { get; set; } 
		public string FullName { get; set; }
		public string? Image { get; set; }
		public string Email { get; set; }
		public UserType UserType { get; set; }
		public double Height { get; set; }
		public double Weight { get; set; }
		public DateTime BDate { get; set; }
		public string Gender { get; set; } 
		public string? MedicalConditions { get; set; }//هل يعاني من امراض مزمنه
		public string? Allergies { get; set; }//حساسيه من اطعمه معينه
		public string Fitness_Goal { get; set; } 
	}
}
