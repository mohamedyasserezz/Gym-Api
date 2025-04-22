using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface ICoachRepository
	{
		public Task<List<Coach>> GetAllAsyncR();
		public Task<Coach?> GetByIdAsyncR(int id);
		public Task<List<Coach>?> GetBySpecializationAsyncR(string specialization);
	}
}
