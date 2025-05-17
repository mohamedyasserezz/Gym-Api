using System.ComponentModel.DataAnnotations;

namespace Gym_Api.DTO
{
	public class CreateNutritionPlanDto
	{
		[Required]
		public string Coach_ID { get; set; }
		[Required]
		public string User_ID { get; set; }
		[Required]
		public string Day { get; set; }
		[Required]
		public int Calories_Needs { get; set; }
		[Required]
		public int Carbs_Needs { get; set; }
		[Required]
		public int Protein_Needs { get; set; }
		[Required]
		public int Fats_Needs { get; set; }
		[Required]	
		public string FirstMeal { get; set; }
		[Required]
		public string SecondMeal { get; set; }
		[Required]
		public string ThirdMeal { get; set; }
		public string? FourthMeal { get; set; }
		public string? FifthMeal { get; set; }
		public string Snacks { get; set; }
		public string? Vitamins { get; set; }
		public string? Notes { get; set; }
		
	}
}
