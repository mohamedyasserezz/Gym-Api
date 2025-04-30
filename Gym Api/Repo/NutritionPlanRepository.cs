using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class NutritionPlanRepository : INutritionPlanRepository
	{
		private readonly AppDbContext _context;
		public NutritionPlanRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> HasActiveSubscriptionAsync(int userId, int coachId)
		{
			return await _context.Subscriptions.AnyAsync(s =>
				s.User_ID == userId &&
				s.Coach_ID == coachId &&
				s.IsPaid == true &&
				s.IsApproved == true &&
				s.EndDate > DateTime.UtcNow);
		}


		public async Task<NutritionPlan> AddNutritionPlanAsync(NutritionPlan plan)
		{
			_context.NutritionPlans.Add(plan);
			await _context.SaveChangesAsync();
		    return plan;
		}


		public async Task<NutritionPlan?> GetUserNutritionPlanByDayAsync(int userId, string day)
		{
			return await _context.NutritionPlans
				.FirstOrDefaultAsync(p => p.User_ID == userId && p.Day == day);
		}




	}
}
