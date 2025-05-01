using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Survices
{
	public interface IAssignmentService
	{
		public Task<string> AddAssignmentAsync(CreateAssignmentDto dto);
		public Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, string day);


	}
}
