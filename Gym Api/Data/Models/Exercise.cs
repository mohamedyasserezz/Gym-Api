using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Exercise
	{
		[Key]
		public int Exercise_ID { get; set; }
		public string Exercise_Name { get; set; }
		public string Description { get; set; }
		public string Video_URL { get; set; }
		public int Duration { get; set; }
		public string Target_Muscle { get; set; }
		public int Difficulty_Level { get; set; }
		public int Calories_Burned { get; set; }

		[ForeignKey("Category")]
		public int Category_ID { get; set; }
		public Category Category { get; set; }
		public ICollection<Assignment> Assignments { get; set; }
	}
}
