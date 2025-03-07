namespace Gym_Api.Contract
{
	public class CreateCoachRequest
	{
		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }
		public string Availability { get; set; }
		public int Experience_Years { get; set; }
		public string Password { get; set; }
		public string Portfolio_Link { get; set; }
		public double Ratings { get; set; }


	}
}
