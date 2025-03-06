using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface ICoachService
	{
		public Task<List<Coach>> GetAllCoachesAsync();
		public Task<Coach?> GetCoachByIdAsync(int id);
		public Task<Coach> CreateCoachAsync(Coach coach);
		public Task<bool> UpdateCoachAsync(int id, Coach updatedCoach);
		public Task<bool> DeleteCoachAsync(int id);
	}
}
