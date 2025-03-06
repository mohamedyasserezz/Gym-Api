using Gym_Api.Data.Models;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoachesController : ControllerBase
	{
		private readonly ICoachService _coachService;

		public CoachesController(ICoachService coachService)
		{
			_coachService = coachService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCoaches()
		{
			var coaches = await _coachService.GetAllCoachesAsync();
			return Ok(coaches);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetCoach(int id)
		{
			var coach = await _coachService.GetCoachByIdAsync(id);
			if (coach == null)
			{
				return NotFound($"coach id {id} not exist");
			}
			return Ok(coach);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCoach([FromBody] Coach coach)
		{
			var newCoach = await _coachService.CreateCoachAsync(coach);
			return Ok(newCoach);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCoach(int id, [FromBody] Coach coach)
		{
			var result = await _coachService.UpdateCoachAsync(id, coach);
			if (!result)
			{
				return NotFound($"Coach with id {id}not found");
			}
			return NoContent();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCoach(int id)
		{
			var result = await _coachService.DeleteCoachAsync(id);
			if (!result)
			{
				return NotFound($"Coachwith id {id} not found");
			}
			return NoContent();
		}
	}
}
