using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class User
	{
		[Key]
		public int User_ID { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public double Height { get; set; }
		public double Weight { get; set; }
		public DateTime BDate { get; set; }
		public string Gender { get; set; }
		public DateTime Payment_History { get; set; }
		public DateTime Registration_Date { get; set; }
		public string Subscription_Status { get; set; }
		public string Fitness_Goal { get; set; }

		public ICollection<Assignment> Assignments = new List<Assignment>();
		public ICollection<Subscribe> Subscriptions = new List<Subscribe>();
		public NutritionPlan NutritionPlan { get; set; }
		public ICollection<Order> Orders = new List<Order>();

	}
}
