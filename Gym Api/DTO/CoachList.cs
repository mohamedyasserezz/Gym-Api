using Gym_Api.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Gym_Api.DTO
{
	public class CoachList
	{
		public string UserId { get; set; }
		public string FullName { get; set; } 
		public string? Image { get; set; } 
		public string Email { get; set; }
		public string Specialization { get; set; } 
		public string Portfolio_Link { get; set; } 
		public int Experience_Years { get; set; }
		public string Availability { get; set; }  
		public string Bio { get; set; } 
		public bool IsConfirmedByAdmin {  get; set; }
	}
}
