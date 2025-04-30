using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;

namespace Gym_Api.Survices
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
			// تحقق من وجود اشتراك ساري
			var hasSubscription = await _repository.HasActiveSubscriptionAsync(dto.User_ID, dto.Coach_ID);
			if (hasSubscription)
			{
				var plan = new NutritionPlan
				{
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
					Notes = dto.Notes,
					Coach_ID = dto.Coach_ID,
					User_ID = dto.User_ID
				};
				await _repository.AddNutritionPlanAsync(plan);
				return "تم إضافة خطه غذائيه بنجاح للمشترك";
			}
			// إضافة مسار عودة في حالة عدم وجود اشتراك ساري
			return "لا يمكن إضافة خطه غذائيه، لا يوجد اشتراك ساري للمستخدم";
		}

		public async Task<NutritionPlan?> GetUserNutritionPlanByDayAsync(int userId, string day)
		{
			return await _repository.GetUserNutritionPlanByDayAsync(userId, day);
		}



	}
}
