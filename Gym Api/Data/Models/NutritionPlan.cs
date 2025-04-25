using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class NutritionPlan
	{
		[Key]
		public int ID { get; set; }

		public int Calories_Needs { get; set; }
		public int Carbs_Needs { get; set; }
		public int Protein_Needs { get; set; }
		public int Fats_Needs { get; set; }
		public string Breakfast { get; set; }
		public string Lunch { get; set; }
		public string Dinner { get; set; }
		public string Snack { get; set; }

		[ForeignKey("Coach")]
		public int Coach_ID { get; set; }
		public Coach coach { get; set; }

		[ForeignKey("User")]
		public int User_ID { get; set; }	
	}
}
