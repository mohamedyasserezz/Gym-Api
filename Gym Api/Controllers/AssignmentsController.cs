using Gym_Api.Contract;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AssignmentsController : ControllerBase
	{
		private readonly IAssignmentService _assignmentservice;
		public AssignmentsController(IAssignmentService assignmentservice)
		{
			_assignmentservice = assignmentservice;
		}



		[HttpPost]
		public async Task<IActionResult> AddAssignment(CreateAssignmentDto createAssignmentDto)
		{
			var newassignment = await _assignmentservice.AddAssignmentAsync(createAssignmentDto);
			return Ok(newassignment);
		}



		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetUserAssignments(int userId)
		{
			var assignments = await _assignmentservice.GetUserAssignmentsAsync(userId);
			return Ok(assignments);
		}



		[HttpGet("coach/{coachId}")]
		public async Task<IActionResult> GetCoachAssignments(int coachId)
		{
			var assignments = await _assignmentservice.GetCoachAssignmentsAsync(coachId);
			return Ok(assignments);
		}



		[HttpPut("complete")]
		public async Task<IActionResult> CompleteAssignment([FromQuery] int userId, [FromQuery] int assignmentId)
		{
			var result = await _assignmentservice.CompleteAssignmentAsync(userId, assignmentId);
			
			if (result == "Assignment not found.")
			{
				return NotFound(result);
			}
			if (result == "Assignment is already marked as completed.")
			{
				return BadRequest(result);
			}

			return Ok(result);
		}


		[HttpPut("update")]
		public async Task<IActionResult> UpdateAssignment(int assignmentId, int coachId, [FromBody] UpdateAssignmentDto updateDto)
		{
			var result = await _assignmentservice.UpdateAssignmentAsync(assignmentId, coachId, updateDto);
			if (result.Contains("not found") || result.Contains("don't have permission"))
				return NotFound(result);
			return Ok(result);
		}



		[HttpDelete("Delete")]
		public async Task<IActionResult> DeleteAssignment(int assignmentId, int CoachId)
		{
			var result = await _assignmentservice.DeleteAssignmentAsync(assignmentId,CoachId);
			if (!result) return NotFound("Assignment not found.");
			return Ok("Assignment deleted successfully.");
		}



	}
}
