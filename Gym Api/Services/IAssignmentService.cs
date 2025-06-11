using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Survices
{
	public interface IAssignmentService
	{
		public Task<Assignment?> GetAssignmentByIdAsync(int id);
		public Task<string> AddAssignmentAsync(BulkCreateAssignmentDto createAssignmentDto); 
		public Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, DateTime day);
		public Task<List<AssignmentViewDto>> GetAllUserAssignmentsAsync(string userId);
		public Task<bool> CompleteAssignmentAsync(int id);


	}
}
