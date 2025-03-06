using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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
		[JsonIgnore]
		[IgnoreDataMember]
		public virtual ICollection<Assignment> Assignments { get; set; }
		[JsonIgnore]
		[IgnoreDataMember]
		public virtual ICollection<Subscribe> Subscriptions { get; set; }
		[JsonIgnore]
		[IgnoreDataMember]
		public virtual ICollection<NutritionPlan> NutritionPlans { get; set; }

	}
}
