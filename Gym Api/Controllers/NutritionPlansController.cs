using Gym_Api.DTO;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

		[HttpPost("AddNutritionPlan")]
		public async Task<IActionResult> AddNutritionPlan([FromForm] CreateNutritionPlanDto dto)
		{
			var plan = await _service.AddNutritionPlanAsync(dto);
			return Ok(plan);
		}



		[HttpGet("user/{userId}/day/{day}")]
		public async Task<IActionResult> GetNutritionPlanByDay(int userId, string day)
		{
			var plan = await _service.GetUserNutritionPlanByDayAsync(userId, day);
			if (plan == null)
				return NotFound($"{day} لا توجد خطه غذائيه لك في يوم");
			return Ok(plan);
		}


	}
}
