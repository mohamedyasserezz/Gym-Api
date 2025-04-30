using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface IAssignmentRepository
	{
		public Task<Assignment> AddAssignmentAsync(Assignment assignment);
		public Task<bool> HasActiveSubscriptionAsync(int userId, int coachId);
		Task<List<Assignment>> GetUserAssignmentsByDayAsync(int userId, string day);



	}
}
