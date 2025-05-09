using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Survices
{
	public interface ICoachService
	{
		public Task<List<Coach>> GetAllCoachesAsync();
		public Task<Coach?> GetCoachByIdAsync(string id);
		public Task<List<Coach>?> GetCoachesBySpecializationAsync(string specialization);
	    public Task<List<Coach>> GetApprovedCoachesAsync();
		public Task<List<Coach>> GetUnapprovedCoachesAsync();
		public Task<int> GetCoachCountAsync();
		public Task<bool> ApproveCoachAsync(string id);
		public Task<bool> UpdateCoachAsync(string coachId, UpdateCoachDto dto);
		public Task<bool> DeleteCoachAsync(string coachId);


	}
}
