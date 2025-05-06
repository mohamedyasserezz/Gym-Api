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
		[HttpGet("GetAllCoaches")]
		public async Task<IActionResult> GetAll()
		{
			var coaches = await _coachService.GetAllCoachesAsync();
			return Ok(coaches);
		}


		// ✅ Get Coach by Id
		[HttpGet("GetCoachbyId{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var coach = await _coachService.GetCoachByIdAsync(id);
			if (coach == null)
			{
				return NotFound($"Coach with id {id} not found");
			}

			return Ok(coach);
		}


		// ✅ Get Coaches by Specialization
		[HttpGet("GetCoachesByspecialization/{specialization}")]
		public async Task<IActionResult> GetBySpecialization(string specialization)
		{
			var coaches = await _coachService.GetCoachesBySpecializationAsync(specialization);
			return Ok(coaches);

		}

		[HttpGet("AllApprovedCoaches")]
		public async Task<IActionResult> GetApprovedCoaches()
		{
			var coaches = await _coachService.GetApprovedCoachesAsync();
			return Ok(coaches);
		}

		[HttpGet("AllUnapprovedCoaches")]
		public async Task<IActionResult> GetUnapprovedCoaches()
		{
			var coaches = await _coachService.GetUnapprovedCoachesAsync();
			return Ok(coaches);
		}

		[HttpGet("GetCoachCount")]

		public async Task<IActionResult> GetCountOfCoach()
		{
			var coaches = await _coachService.GetCoachCountAsync();
			return Ok(coaches);
		}

		[HttpPut("AdminApprovesCoach{id}")]
		public async Task<IActionResult> ApproveCoach(string id)
		{
			var result = await _coachService.ApproveCoachAsync(id);
			if (!result)
				return NotFound("Coach not found or already approved.");

			return Ok("Coach approved successfully.");
		}


	}
}
