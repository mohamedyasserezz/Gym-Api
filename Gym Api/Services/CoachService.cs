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



		public async Task<Coach?> GetCoachByIdAsync(string id)
		{
			return await _repository.GetByIdAsyncR(id);
		}


		public async Task<List<Coach>?> GetCoachesBySpecializationAsync(string specialization)
		{
			return await _repository.GetBySpecializationAsyncR(specialization);
		}


		public async Task<List<Coach>> GetApprovedCoachesAsync()
		{
			return await _repository.GetApprovedCoachesAsyncR();
		}


		public async Task<List<Coach>> GetUnapprovedCoachesAsync()
		{
		   return await	_repository.GetUnapprovedCoachesAsyncR();
		}


		public async Task<bool> ApproveCoachAsync(string id)
		{
			var coach = await _repository.GetByIdAsyncR(id);
			if(coach == null || coach.IsConfirmedByAdmin)
			{
				return false;
			}
			coach.IsConfirmedByAdmin = true;
			await _repository.ApproveCoachAsyncR(coach);
			return true;

		}


	}
}
