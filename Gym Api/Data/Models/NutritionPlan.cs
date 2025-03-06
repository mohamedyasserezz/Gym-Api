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

		[ForeignKey("Coach")]
		public int Coach_ID { get; set; }
		public Coach Coach { get; set; }

		[ForeignKey("User")]
		public int User_ID { get; set; }
		public User User { get; set; }
	}
}
