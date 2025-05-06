using Gym_Api.Data.Models;

namespace Gym_Api.Services
{
	public interface IUserService
	{
		public Task<List<User>> GetAllUsersAsync();
		public Task<User> GetUserByidAsync(string id);
		public Task<int> GetUsersCountAsync();

	}
}
