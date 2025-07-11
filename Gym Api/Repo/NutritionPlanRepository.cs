﻿using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class NutritionPlanRepository : INutritionPlanRepository
	{
		private readonly ApplicationDbContext _context;
		public NutritionPlanRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> HasActiveSubscriptionAsync(string userId, string coachId)
		{
			return await _context.Subscriptions.AnyAsync(s =>
				s.User_ID == userId &&
				s.Coach_ID == coachId &&
				s.IsPaid == true &&
				s.IsApproved == true &&
				s.EndDate > DateTime.UtcNow);
		}


		public async Task<NutritionPlan> GetNutritionPlanByUserAndDayAsync(string userId, string day)
		{
			return await _context.NutritionPlans
				.FirstOrDefaultAsync(np => np.User_ID == userId && np.Day == day);
		}

		public async Task<NutritionPlan> AddNutritionPlanAsync(NutritionPlan plan)
		{
			_context.NutritionPlans.Add(plan);
			await _context.SaveChangesAsync();
		    return plan;
		}


		public async Task<List<NutritionPlan>> GetAllUserNutritionPlansAsync(string userId)
		{
			return await _context.NutritionPlans
				.Where(np => np.User_ID == userId)
				.ToListAsync();
		}


	}
}
