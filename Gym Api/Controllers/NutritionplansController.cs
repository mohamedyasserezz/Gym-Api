using Gym_Api.Contract;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NutritionplansController : ControllerBase
	{
		private readonly INutritionplanService _nutritionplanService;
		public NutritionplansController(INutritionplanService nutritionplanService)
		{
			_nutritionplanService = nutritionplanService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllNutritionplans()
		{
			var nutritionplans = await _nutritionplanService.GetAllNutritionplanAsync();
			return Ok(nutritionplans);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetNutritionplanById(int id)
		{
			var nutritionplan = await _nutritionplanService.GetNutritionPlanByIdAsync(id);
			if (nutritionplan == null)
			{
				return NotFound($"nutritionplan with Id {id} Not Exist");
			}
			return Ok(nutritionplan);
		}



		[HttpPost]
		public async Task<IActionResult> AddNewNutritionplan(CreateNewNutritionplan createNewNutritionplan)
		{
			var NewNutritionplan = await _nutritionplanService.AddNewNutritionplanAsync(createNewNutritionplan);
			return Ok(NewNutritionplan);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateNutritionplan([FromRoute]int id,[FromBody] UpdateNutritionplan updateNutritionplan)
		{
			var updatenutrition = await _nutritionplanService.UpdateNutritionplanAsync(id, updateNutritionplan);
			if (!updatenutrition)
			{
				return NotFound("Nutritionplan Not Exist");
			}
			return Ok("Nutritionplan updated successfully");
		}


		[HttpDelete]
		public async Task<IActionResult> DeleteNutritionplan(int id)
		{
			var nutritionplan = await _nutritionplanService.DeleteNutritionplanAsync(id);
			if (!nutritionplan)
			{
				return NotFound("Nutritionplan Not Exist");
			}
			return Ok("Nutritionplan deleted successfully");
		}
	}
}
