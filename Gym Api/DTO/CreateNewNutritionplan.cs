namespace Gym_Api.Contract
{
	public class CreateNewNutritionplan
	{
		public int Calories_Needs { get; set; }
		public int Carbs_Needs { get; set; }
		public int Protein_Needs { get; set; }
		public int Fats_Needs { get; set; }
		public int User_ID { get; set; }
		public int Coach_ID { get; set; }


	}
}
