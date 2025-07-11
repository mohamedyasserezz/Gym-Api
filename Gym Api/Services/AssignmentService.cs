﻿using Gym_Api.Contract;
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

		public async Task<Assignment?> GetAssignmentByIdAsync(int id)
		{
			return await _repository.GetByIdAsyncR(id);
		}



		public async Task<string> AddAssignmentAsync(BulkCreateAssignmentDto createAssignmentDto)
		{
			// التحقق من وجود اشتراك ساري
			var hasActiveSubscription = await _repository.HasActiveSubscriptionAsync(createAssignmentDto.UserId, createAssignmentDto.CoachId);
			if (!hasActiveSubscription)
			{
				return "لا يمكن إضافة مهمه، لا يوجد اشتراك ساري للمستخدم";
			}

			foreach (var assignmentDto in createAssignmentDto.Assignments)
			{
				// التحقق من وجود مهمة بنفس اليوم
				var existingAssignment = await _repository.GetAssignmentByUserAndDayAsync(createAssignmentDto.UserId, assignmentDto.Day);
				if (existingAssignment != null)
				{
					return "لا يمكن إضافة مهمة في نفس اليوم";
				}

				if (assignmentDto.ExerciseIds == null || !assignmentDto.ExerciseIds.Any())
				{
					return "لا يمكن إضافة مهمة بدون تمارين. يرجى إدخال قائمة تمارين غير فارغة.";
				}

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

			return "تم إضافة المهام للمشترك بنجاح";
		}



		public async Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, DateTime day)
		{
			return await _repository.GetUserAssignmentsByDayAsync(userId, day);
		}


		public async Task<List<AssignmentViewDto>> GetAllUserAssignmentsAsync(string userId)
		{
			return await _repository.GetAllUserAssignmentsAsync(userId);
		}



		public async Task<bool> CompleteAssignmentAsync(int id)
		{
			var assignment = await _repository.GetByIdAsyncR(id);
			if (assignment == null || assignment.IsCompleted)
			{
				return false;
			}
			assignment.IsCompleted = true;
			await _repository.CompleteAssignmetAsyncR(assignment);
			return true;
		}

	}
}
