using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
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


		public async Task<int> GetCoachCountAsync()
		{
			return await _repository.GetCoachCount();
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


		public async Task<bool> UpdateCoachAsync(string coachId, UpdateCoachDto dto)
		{
			var coach = await _repository.GetByIdAsyncR(coachId);
			if (coach == null) return false;

			coach.Specialization = dto.Specialization;
			coach.Portfolio_Link = dto.Portfolio_Link;
			coach.Experience_Years = dto.Experience_Years;
			coach.Availability = dto.Availability;
			coach.Bio = dto.Bio;

			await _repository.UpdateAsync(coach);
			return true;
		}


		public async Task<bool> DeleteCoachAsync(string coachId)
		{
			var coach = await _repository.GetByIdAsyncR(coachId);
			if (coach == null) return false;

			await _repository.DeleteAsync(coach);
			return true;
		}



	}
}
