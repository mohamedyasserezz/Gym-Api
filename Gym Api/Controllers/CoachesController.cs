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

		[HttpGet]
		public async Task<IActionResult> GetAllCoaches()
		{
			var Coaches = await _coachService.GetAllCoachesAsync();
			return Ok(Coaches);
		}


		[HttpGet("AllApprovedCoaches")]
		public async Task<IActionResult> GetAllApprovedCoaches()
		{
			var coaches = await _coachService.GetAllApprovedCoachesAsync();
			return Ok(coaches);
		}


		[HttpGet("CoachId{CoachId}")]
		public async Task<IActionResult> GeyCoachById(int CoachId)
		{
			var coach = await _coachService.GetCoachByIdAsync(CoachId);
			if (coach == null) 
			{
				return NotFound($"CoachId {CoachId} Not Exist");
			}
			return Ok(coach);
		}



		[HttpPut("approve/{coachId}")]
		public async Task<IActionResult> Approvecoach(int coachId)
		{
			var result = await _coachService.ApproveCoachAsync(coachId);
			if (result == "Coach Not Exist") 
			{
				return NotFound(result);
			}
			if (result == "This subscription is already approved")
			{
				return BadRequest(result);
			}
			return Ok(result);
		}


		[HttpPut("reject/{CoachId}")]
		public async Task<IActionResult> RejectSubscription(int CoachId)
		{
			var result = await _coachService.RejectCoachAsync(CoachId);

			if (result == "Coach Not Exist")
			{
				return NotFound(result);
			}
			if(result == "The coach cannot be refused after being approved")
			{
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpDelete("{coachId}")]
		public async Task<IActionResult> DeleteCoach(int coachId)
		{
			var result = await _coachService.DeleteCoachAsync(coachId);
			if (!result) return NotFound($"CoachId {coachId} not exist");
			return Ok("Deleted successfuly");
		}

	}
}
