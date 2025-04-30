using Gym_Api.DTO;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssignmentsController : ControllerBase
	{
		private readonly IAssignmentService _assignmentService;

		public AssignmentsController(IAssignmentService assignmentService)
		{
			_assignmentService = assignmentService;
		}




		[HttpPost("AddNewAssignmentForUser")]
		public async Task<IActionResult> AddAssignment([FromForm] CreateAssignmentDto dto)
		{
			var result = await _assignmentService.AddAssignmentAsync(dto);
			return Ok(result);
		}



		[HttpGet("user/{userId}/day/{day}")]
		public async Task<IActionResult> GetUserAssignmentsByDay(int userId, string day)
		{
			var assignments = await _assignmentService.GetUserAssignmentsByDayAsync(userId, day);

			if (assignments == null || !assignments.Any())
				return NotFound($"{day} لا توجد مهام لك في يوم");

			return Ok(assignments);
		}



	}
}
