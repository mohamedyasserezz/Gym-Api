using Gym_Api.Common.Consts;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
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


		public async Task<List<UserList>> GetAllUsersAsync()
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


		public async Task<bool> UpdateUserdataAsync(string Userid, UpdateUserDto dto)
		{
			var user = await _repository.GetUsersById(Userid);
			if (user == null) return false;

			user.Height = dto.Height;
			user.Weight = dto.Weight;
			user.BDate = dto.BDate;
			user.Gender = dto.Gender;
			user.MedicalConditions = dto.MedicalConditions;
			user.Allergies = dto.Allergies;
			user.Fitness_Goal = dto.Fitness_Goal;

			await _repository.UpdateUserAsync(user);
			return true;
		}


		public async Task<bool> DeleteUserdataAsync(string userId)
		{
			var user = await _repository.GetUsersById(userId);
			if (user == null) return false;

			await _repository.DeleteUserAsync(user);
			return true;
		}

	}
}
