﻿using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class User
	{
		[Key]
		public string UserId { get; set; } = default!;
		public ApplicationUser ApplicationUser { get; set; } = default!;
		public double Height { get; set; }
		public double Weight { get; set; }
		public DateTime BDate { get; set; }
		public string Gender { get; set; } = default!;
		public string? MedicalConditions { get; set; }//هل يعاني من امراض مزمنه
		public string? Allergies { get; set; }//حساسيه من اطعمه معينه
		public string Fitness_Goal { get; set; } = default!;
		public string InBody { get; set; }

		public ICollection<Assignment> Assignments = new List<Assignment>();
		public ICollection<Subscribe> Subscriptions = new List<Subscribe>();
		public ICollection<NutritionPlan> NutritionPlans = new List<NutritionPlan>();
		public ICollection<Order> Orders = new List<Order>();

	}
}
