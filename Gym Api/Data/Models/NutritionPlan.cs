using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class NutritionPlan
	{
		[Key]
		public int ID { get; set; }
		public string Day { get; set; }
		public int Calories_Needs { get; set; }
		public int Carbs_Needs { get; set; }
		public int Protein_Needs { get; set; }
		public int Fats_Needs { get; set; }
		public string FirstMeal { get; set; }
		public string SecondMeal { get; set; }
		public string ThirdMeal { get; set; }
		public string? FourthMeal { get; set; }
		public string? FifthMeal { get; set; }
		public string Snacks { get; set; }
		public string? Vitamins { get; set; }
		public string? Notes { get; set; }

		

		[ForeignKey("Coach")]
		public string Coach_ID { get; set; }
		public Coach coach { get; set; }

		[ForeignKey("User")]
		public string User_ID { get; set; }
		public User user { get; set; }
	}
}
