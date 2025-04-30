using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface INutritionPlanRepository
	{
		public Task<NutritionPlan> AddNutritionPlanAsync(NutritionPlan plan);
		public Task<bool> HasActiveSubscriptionAsync(int userId, int coachId);
		public Task<NutritionPlan?> GetUserNutritionPlanByDayAsync(int userId, string day);

	}
}
