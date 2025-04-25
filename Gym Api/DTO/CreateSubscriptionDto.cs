namespace Gym_Api.Contract
{
	public class CreateSubscriptionDto
	{
		public int UserId { get; set; }
		public int CoachId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
