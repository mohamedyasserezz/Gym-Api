using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Repo
{
	public interface ICoachRepository
	{
		public Task<List<CoachList>> GetAllAsyncR();
		public Task<Coach?> GetByIdAsyncR(string id);
		public Task<List<CoachList>?> GetBySpecializationAsyncR(string specialization);
		public Task<List<CoachList>> GetApprovedCoachesAsyncR();
		public Task<List<CoachList>> GetUnapprovedCoachesAsyncR();
		public Task<int> GetCoachCount();
		public Task<bool> ApproveCoachAsyncR(Coach coach);
		public Task<bool> UpdateAsync(Coach coach);
		public Task<bool> DeleteAsync(Coach coach);
	
	}
}
