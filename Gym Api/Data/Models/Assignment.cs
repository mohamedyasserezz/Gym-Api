﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.Data.Models
{
	public class Assignment
	{
		[Key]
		public int Assignment_ID { get; set; }
		public DateTime Day { get; set; } 
		public bool IsCompleted { get; set; } = false;
		public string? Notes { get; set; }

		[ForeignKey("User")]
		public string User_ID { get; set; }
		public User User { get; set; }

		[ForeignKey("Coach")]
		public string Coach_ID { get; set; }
		public Coach Coach { get; set; }

		public ICollection<AssignmentExercise> AssignmentExercises { get; set; } = new List<AssignmentExercise>();

	}
}
