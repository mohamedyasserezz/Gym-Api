using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;
using Gym_Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class AssignmentService : IAssignmentService
	{
		private readonly IAssignmentRepository _repository;
		public AssignmentService(IAssignmentRepository repository) 
		{
			_repository = repository;
		}


		public async Task<string> AddAssignmentAsync(CreateAssignmentDto createAssignmentDto)
		{
			var subscribtion = await _repository.HasActiveSubscriptionAsync(createAssignmentDto.UserId,createAssignmentDto.CoachId);
			if (subscribtion)
			{
				var newAssignment = new Assignment
				{
					User_ID = createAssignmentDto.UserId,
					Coach_ID = createAssignmentDto.CoachId,
					Exercise_ID = createAssignmentDto.ExerciseId,
					Day = createAssignmentDto.Day,
					Notes = createAssignmentDto.Notes
				};
				await _repository.AddAssignmentAsync(newAssignment);
				return "تم إضافة المهمة للمشترك بنجاح";
			}
			return "لا يمكن إضافة المهمة، لا يوجد اشتراك ساري للمستخدم";
		}


		public async Task<List<Assignment>> GetUserAssignmentsByDayAsync(int userId, string day)
		{
			return await _repository.GetUserAssignmentsByDayAsync(userId, day);
		}

	}
}
