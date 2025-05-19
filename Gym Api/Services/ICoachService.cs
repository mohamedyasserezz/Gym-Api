using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Survices
{
	public interface ICoachService
	{
		public Task<List<CoachList>> GetAllCoachesAsync();
		public Task<Coach?> GetCoachByIdAsync(string id);
		public Task<List<CoachList>?> GetCoachesBySpecializationAsync(string specialization);
	    public Task<List<CoachList>> GetApprovedCoachesAsync();
		public Task<List<CoachList>> GetUnapprovedCoachesAsync();
		public Task<int> GetCoachCountAsync();
		public Task<bool> ApproveCoachAsync(string id);
		public Task<bool> UpdateCoachAsync(string coachId, UpdateCoachDto dto);
		public Task<bool> DeleteCoachAsync(string coachId);


	}
}
