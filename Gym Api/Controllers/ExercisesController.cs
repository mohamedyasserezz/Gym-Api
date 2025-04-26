using Gym_Api.Contract;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Migrations;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExercisesController : ControllerBase
	{
		private readonly IExerciseSurvice _exerciseSurvice;
		public ExercisesController(IExerciseSurvice exerciseSurvice) 
		{
			_exerciseSurvice = exerciseSurvice;
		}



		// ✅ Get All Exercises
		[HttpGet("GetAllExercises")]
		public async Task<IActionResult> GetAll()
		{
			var exercises = await _exerciseSurvice.GetAllExercisesAsync();
			return Ok(exercises);
		}


		[HttpGet("GetExerciseById{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var exercise = await _exerciseSurvice.GetExerciseByIdAsync(id);
			if(exercise == null)
			{
				return NotFound($"Exercise with id {id} not found");
			}	
			return Ok(exercise);
		}


		// ✅ Get by Name
		[HttpGet("GetExerciseByName{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var exercise = await _exerciseSurvice.GetExerciseByNameAsync(name);
			if (exercise == null)
				return NotFound($"Exercise '{name}' not found.");

			return Ok(exercise);
		}




		// ✅ Add New Exercise
		[HttpPost("AddNewExercise")]
		public async Task<IActionResult> AddExercise([FromForm]CreateNewExerciseDto dto)
		{
			var newExercise = await _exerciseSurvice.AddExerciseAsync(dto);
			return Ok(newExercise);
		}




		[HttpPut("updateExercise{id}")]
		public async Task<IActionResult> UpdateExercise(int id, [FromForm] Updateexercise dto)
		{
			var result = await _exerciseSurvice.UpdateExerciseAsync(id, dto);
			if (!result)
				return NotFound($"Exercise with id {id} not found.");

			return Ok("Exercise updated successfully.");
		}



		[HttpDelete("DeleteExercise{id}")]
		public async Task<IActionResult> DeleteExercise(int id)
		{
			var result = await _exerciseSurvice.DeleteExerciseAsync(id);
			if (!result)
				return NotFound($"Exercise with id {id} not found.");

			return Ok("Exercise deleted successfully.");
		}


	}
}
