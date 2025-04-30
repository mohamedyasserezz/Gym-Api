using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface ICoachService
	{
		public Task<List<Coach>> GetAllCoachesAsync();
		public Task<Coach?> GetCoachByIdAsync(int id);
		public Task<List<Coach>?> GetCoachesBySpecializationAsync(string specialization);
	    public Task<List<Coach>> GetApprovedCoachesAsync();
		public Task<List<Coach>> GetUnapprovedCoachesAsync();
		public Task<bool> ApproveCoachAsync(int id);



	}
}
