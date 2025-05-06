using Gym_Api.Data.Models;
using Gym_Api.Repo;

namespace Gym_Api.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repository;
		public UserService(IUserRepository repository)
		{
			_repository = repository;
		}
		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _repository.GetAllUsers();
		}

		public async Task<User> GetUserByidAsync(string id)
		{
			return await _repository.GetUsersById(id);
		}

		public async Task<int> GetUsersCountAsync()
		{
			return await _repository.GetUserCount();
		}

	}
}
