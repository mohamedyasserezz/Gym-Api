﻿using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Services
{
	public interface INutritionPlanService
	{
		public Task<string> AddNutritionPlanAsync(CreateNutritionPlanDto dto);

		public Task<List<NutritionPlan>> GetAllUserNutritionPlansAsync(string userId);
	}
}
