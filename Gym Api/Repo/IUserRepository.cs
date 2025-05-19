using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Repo
{
	public interface IUserRepository
	{
		public Task<List<UserList>> GetAllUsers();
		public Task<User> GetUsersById(string id);
		public Task<int> GetUserCount();
		public Task<bool> UpdateUserAsync(User user);
		public Task<bool> DeleteUserAsync(User user);

	}
}
