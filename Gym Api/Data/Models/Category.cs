using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Category
	{
		[Key]
		public int Category_ID { get; set; }
		public string Category_Name { get; set; }
		public string ImageUrl { get; set; }
		public ICollection<Exercise> Exercises = new List<Exercise>();
	}
}
