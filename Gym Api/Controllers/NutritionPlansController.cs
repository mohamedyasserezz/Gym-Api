using Gym_Api.DTO;
using Gym_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NutritionPlansController : ControllerBase
	{
		private readonly INutritionPlanService _service;

		public NutritionPlansController(INutritionPlanService service)
		{
			_service = service;
		}


		[HttpPost("AddNutritionPlanForUser")]
		public async Task<IActionResult> AddNutritionPlan([FromBody] CreateNutritionPlanDto dto)
		{
			var plan = await _service.AddNutritionPlanAsync(dto);
			return Ok(plan);
		}



		[HttpGet("GetAllUserNutritionplans/{userId}")]
		public async Task<IActionResult> GetAllUserNutritionPlans(string userId)
		{
			var plans = await _service.GetAllUserNutritionPlansAsync(userId);

			if (plans == null || !plans.Any())
			{
				return Ok(new { message = $"{userId} لا توجد خطه غذائيه للمستخدم" });
			}

			return Ok(plans);
		}







	}
}
