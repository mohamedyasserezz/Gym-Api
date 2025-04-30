using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.Repo;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class CoachService : ICoachService
	{
		private readonly ICoachRepository _repository;
		public CoachService(ICoachRepository repository)
		{
			_repository = repository;
		}



		public async Task<List<Coach>> GetAllCoachesAsync()
		{
			return await _repository.GetAllAsyncR();
		}



		public async Task<Coach?> GetCoachByIdAsync(int id)
		{
			return await _repository.GetByIdAsyncR(id);
		}



		public async Task<List<Coach>?> GetCoachesBySpecializationAsync(string specialization)
		{
			return await _repository.GetBySpecializationAsyncR(specialization);
		}


	}
}
