using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Repo
{
	public interface IAssignmentRepository
	{
		public Task<Assignment> AddAssignmentAsync(Assignment assignment);
		public Task<bool> HasActiveSubscriptionAsync(string userId, string coachId);
		Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, string day);
		public Task<List<AssignmentViewDto>> GetAllUserAssignmentsAsync(string userId);



	}
}
