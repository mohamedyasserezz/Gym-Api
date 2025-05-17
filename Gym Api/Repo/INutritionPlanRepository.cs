using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface INutritionPlanRepository
	{
		public Task<NutritionPlan> AddNutritionPlanAsync(NutritionPlan plan);
		public Task<bool> HasActiveSubscriptionAsync(string userId, string coachId);
		public Task<List<NutritionPlan>> GetAllUserNutritionPlansAsync(string userId);

	}
}
