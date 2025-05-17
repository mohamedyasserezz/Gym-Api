using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;

namespace Gym_Api.Services
{
	public class NutritionPlanService : INutritionPlanService
	{
		private readonly INutritionPlanRepository _repository;

		public NutritionPlanService(INutritionPlanRepository repository)
		{
			_repository = repository;
		}

		public async Task<string> AddNutritionPlanAsync(CreateNutritionPlanDto dto)
		{
			var subscribtion = await _repository.HasActiveSubscriptionAsync(dto.User_ID, dto.Coach_ID);
			if (subscribtion)
			{
				var plan = new NutritionPlan
				{
					Coach_ID = dto.Coach_ID,
					User_ID = dto.User_ID,
					Day = dto.Day,
					Calories_Needs = dto.Calories_Needs,
					Carbs_Needs = dto.Carbs_Needs,
					Protein_Needs = dto.Protein_Needs,
					Fats_Needs = dto.Fats_Needs,
					FirstMeal = dto.FirstMeal,
					SecondMeal = dto.SecondMeal,
					ThirdMeal = dto.ThirdMeal,
					FourthMeal = dto.FourthMeal,
					FifthMeal = dto.FifthMeal,
					Snacks = dto.Snacks,
					Vitamins = dto.Vitamins,
					Notes = dto.Notes
				};

				await _repository.AddNutritionPlanAsync(plan);
				return "تم اضافه خطه غذائيه بنجاح للمشترك";
			}
			return "لا يمكن إضافة خطه غذائيه، لا يوجد اشتراك ساري للمستخدم";
		}


		public async Task<List<NutritionPlan>> GetAllUserNutritionPlansAsync(string userId)
		{
			return await _repository.GetAllUserNutritionPlansAsync(userId);
		}


	}
}
