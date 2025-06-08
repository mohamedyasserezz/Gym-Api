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


		public async Task<string> AddAssignmentAsync(BulkCreateAssignmentDto createAssignmentDto)
		{
			// التحقق من وجود اشتراك ساري
			var hasActiveSubscription = await _repository.HasActiveSubscriptionAsync(createAssignmentDto.UserId, createAssignmentDto.CoachId);
			if (!hasActiveSubscription)
			{
				return "لا يمكن إضافة المهام، لا يوجد اشتراك ساري للمستخدم";
			}

			// إضافة كل التمارين
			foreach (var assignmentDto in createAssignmentDto.Assignments)
			{
				var newAssignment = new Assignment
				{
					User_ID = createAssignmentDto.UserId,
					Coach_ID = createAssignmentDto.CoachId,
					Day = assignmentDto.Day,
					Notes = assignmentDto.Notes,
					AssignmentExercises = assignmentDto.ExerciseIds.Select(exId => new AssignmentExercise
					{
						Exercise_ID = exId
					}).ToList()
				};

				await _repository.AddAssignmentAsync(newAssignment);
			}

			return "تم اضافه المهام للمشترك بنجاح";
		}


		public async Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, string day)
		{
			return await _repository.GetUserAssignmentsByDayAsync(userId, day);
		}


		public async Task<List<AssignmentViewDto>> GetAllUserAssignmentsAsync(string userId)
		{
			return await _repository.GetAllUserAssignmentsAsync(userId);
		}


	}
}
