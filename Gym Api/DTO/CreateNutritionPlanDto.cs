namespace Gym_Api.DTO
{
	public class CreateNutritionPlanDto
	{
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
		public string Coach_ID { get; set; }
		public string User_ID { get; set; }
	}
}
