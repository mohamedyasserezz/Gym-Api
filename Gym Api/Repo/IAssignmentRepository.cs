﻿using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Repo
{
	public interface IAssignmentRepository
	{
		public Task<Assignment?> GetByIdAsyncR(int id);
		public Task<Assignment> AddAssignmentAsync(Assignment assignment);
		public Task<bool> HasActiveSubscriptionAsync(string userId, string coachId);
		Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, DateTime day);
		public Task<Assignment> GetAssignmentByUserAndDayAsync(string userId, DateTime day);
		public Task<List<AssignmentViewDto>> GetAllUserAssignmentsAsync(string userId);
		public Task<bool> CompleteAssignmetAsyncR(Assignment assignment);




	}
}
