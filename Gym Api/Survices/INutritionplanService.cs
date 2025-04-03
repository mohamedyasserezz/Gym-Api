using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface INutritionplanService
	{
		public Task<List<NutritionPlan>> GetAllNutritionplanAsync();
		public Task<NutritionPlan?> GetNutritionPlanByIdAsync(int id,int CoachId);
		public Task<NutritionPlan> AddNewNutritionplanAsync(CreateNewNutritionplan createNewNutritionplan);
		public Task<bool> UpdateNutritionplanAsync(int id,UpdateNutritionplan updateNutritionplan);
		public Task<bool> DeleteNutritionplanAsync(int id);
	}
}
