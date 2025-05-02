using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Coach
	{
		[Key]
		public string UserId { get; set; } = default!;
		public ApplicationUser ApplicationUser { get; set; } = default!;
		public string Specialization { get; set; } = null!;
		public string Portfolio_Link { get; set; } = default!;
		public int Experience_Years { get; set; }
		public string Availability { get; set; } = default!;
		public string Bio { get; set; } = default!;
		public bool IsApproved { get; set; } = false;
        public bool IsConfirmedByAdmin { get; set; } = false;

        public ICollection<Assignment> Assignments = new List<Assignment>();
		
		public ICollection<Subscribe> Subscriptions = new List<Subscribe>();
		
		public ICollection<NutritionPlan> NutritionPlans = new List<NutritionPlan>();

	}
}
