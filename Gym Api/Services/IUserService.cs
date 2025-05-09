using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Services
{
	public interface IUserService
	{
		public Task<List<User>> GetAllUsersAsync();
		public Task<User> GetUserByidAsync(string id);
		public Task<int> GetUsersCountAsync();
		public Task<bool> UpdateUserdataAsync(string userId, UpdateUserDto dto);
		public Task<bool> DeleteUserdataAsync(string userId);

	}
}
