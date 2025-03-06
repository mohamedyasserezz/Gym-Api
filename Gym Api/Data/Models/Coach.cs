using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Coach
	{
		[Key]
		public int Coach_ID { get; set; }
		public string Fname { get; set; }
		public string Lname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Portfolio_Link { get; set; }
		public int Experience_Years { get; set; }
		public string Availability { get; set; }
		public double Ratings { get; set; }

		public ICollection<Assignment> Assignments { get; set; }
		public ICollection<Subscribe> Subscriptions { get; set; }
		public ICollection<NutritionPlan> NutritionPlans { get; set; }

	}
}
