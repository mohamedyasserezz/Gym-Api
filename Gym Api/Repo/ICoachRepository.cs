using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface ICoachRepository
	{
		public Task<List<Coach>> GetAllAsyncR();
		public Task<Coach?> GetByIdAsyncR(string id);
		public Task<List<Coach>?> GetBySpecializationAsyncR(string specialization);
		public Task<List<Coach>> GetApprovedCoachesAsyncR();
		public Task<List<Coach>> GetUnapprovedCoachesAsyncR();
		public Task<int> GetCoachCount();
		public Task<bool> ApproveCoachAsyncR(Coach coach);
		public Task<bool> UpdateAsync(Coach coach);
		public Task<bool> DeleteAsync(Coach coach);
	}
}
