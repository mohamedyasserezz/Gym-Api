using Gym_Api.DTO;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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




		[HttpPost("AddNewAssignmentsForUser")]
		public async Task<IActionResult> AddAssignments([FromBody] BulkCreateAssignmentDto dto)
		{
			if (dto.Assignments == null || !dto.Assignments.Any())
			{
				return BadRequest("يجب إدخال قائمة التمارين");
			}

			var result = await _assignmentService.AddAssignmentAsync(dto);
			return Ok(result);
		}


		[HttpGet("user/{userId}/day/{day}")]
		public async Task<IActionResult> GetUserAssignmentsByDay(string userId, string day)
		{
			var assignments = await _assignmentService.GetUserAssignmentsByDayAsync(userId, day);

			// لو القايمة فاضية أو null، رجّع NotFound مع رسالة
			if (assignments == null || !assignments.Any())
			{
				return NotFound($"لا توجد مهام لك في يوم {day}");
			}

			return Ok(assignments);
		}


		[HttpGet("GetAllUserAssignments/{userId}")]
		public async Task<IActionResult> GetAllUserAssignments(string userId)
		{
			var assignments = await _assignmentService.GetAllUserAssignmentsAsync(userId);

			if (assignments == null || !assignments.Any())
			{
				return Ok(new { message = $"{userId} لا توجد مهام للمستخدم" });
			}

			return Ok(assignments);
		}
	}
}
