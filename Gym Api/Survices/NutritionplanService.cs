using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class NutritionplanService : INutritionplanService
	{
		private readonly AppDbContext _context;
		public NutritionplanService(AppDbContext context) 
		{
			_context = context;
		}

		public async Task<List<NutritionPlan>> GetAllNutritionplanAsync() 
		{
			return await _context.NutritionPlans.ToListAsync();
		}

		public async Task<NutritionPlan?> GetNutritionPlanByIdAsync(int id) 
		{
			return await _context.NutritionPlans.FirstOrDefaultAsync(c => c.ID == id);
		}

		public async Task<NutritionPlan> AddNewNutritionplanAsync(CreateNewNutritionplan createNewNutritionplan) 
		{
			var newnutritionplan = new NutritionPlan
			{
				Calories_Needs = createNewNutritionplan.Calories_Needs,
				Carbs_Needs = createNewNutritionplan.Carbs_Needs,
				Protein_Needs = createNewNutritionplan.Protein_Needs,
				Fats_Needs = createNewNutritionplan.Fats_Needs,
				User_ID= createNewNutritionplan.User_ID,
				Coach_ID= createNewNutritionplan.Coach_ID
			};
			_context.NutritionPlans.Add(newnutritionplan);
			await _context.SaveChangesAsync();
			return newnutritionplan;
		}

		public async Task<bool> UpdateNutritionplanAsync(int id, UpdateNutritionplan updateNutritionplan) 
		{
			var update = await _context.NutritionPlans.FindAsync(id);
            if (update == null)
            {
				return false;
            }
			update.Calories_Needs = updateNutritionplan.Calories_Needs;
			update.Carbs_Needs = updateNutritionplan.Carbs_Needs;
			update.Protein_Needs = updateNutritionplan.Protein_Needs;
			update.Fats_Needs = updateNutritionplan.Fats_Needs;
			await _context.SaveChangesAsync();
			return true;
        }

		public async Task<bool> DeleteNutritionplanAsync(int id) 
		{
			var nutritionplan = await _context.NutritionPlans.FindAsync(id);
			if (nutritionplan == null)
			{
				return false;
			}
			_context.NutritionPlans.Remove(nutritionplan);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
