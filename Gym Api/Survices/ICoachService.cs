using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface ICoachService
	{
		public Task<List<Coach>> GetAllCoachesAsync();
		public Task<List<Coach>> GetAllApprovedCoachesAsync();
		public Task<Coach> GetCoachByIdAsync(int coachId);
		public Task<string> ApproveCoachAsync(int coachId);
		public Task<string> RejectCoachAsync(int coachId);
		Task<bool> DeleteCoachAsync(int coachId);




	}
}
