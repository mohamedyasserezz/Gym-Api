using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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


		// ✅ Get All Coaches
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var coaches = await _coachService.GetAllCoachesAsync();
			return Ok(coaches);
		}


		// ✅ Get Coach by Id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var coach = await _coachService.GetCoachByIdAsync(id);
			if (coach == null)
			{
				return NotFound($"Coach with id {id} not found");
			}

			return Ok(coach);
		}


		// ✅ Get Coaches by Specialization
		[HttpGet("specialization/{specialization}")]
		public async Task<IActionResult> GetBySpecialization(string specialization)
		{
			var coaches = await _coachService.GetCoachesBySpecializationAsync(specialization);
			return Ok(coaches);

		}


	}
}
