using Gym_Api.Contract;
using Gym_Api.Data.Models;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
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

		[HttpGet]
		public async Task<IActionResult> GetAllExercise() 
		{
			var Exercises = await _exerciseSurvice.GetAllExercisesAsync();
			return Ok(Exercises);
		}


		[HttpGet("{ExerciseName}")]
		public async Task<IActionResult> GetExerciseByname(string ExerciseName) 
		{
			var Exercise = await _exerciseSurvice.GetExercisesByNameAsync(ExerciseName);
			if (Exercise == null) 
			{
				return NotFound($"the Exercise {ExerciseName} Not Exist");
			}
			return Ok(Exercise);
		}


		[HttpPost]
		public async Task<IActionResult> AddNewExercise([FromBody]CreateNewExerciseDto createNewExerciseDto)
		{
			var Exercise = await _exerciseSurvice.AddNewExerciseAsync(createNewExerciseDto);
			return Ok(Exercise);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory([FromRoute]int id,[FromBody]UpdateExerciseDto exerciseDto)
		{
			var updateexercise = await _exerciseSurvice.UpdateExerciseAsync(id, exerciseDto);
			if (!updateexercise) 
			{
				return NotFound("The exercise not found");
			}
			return Ok("Exercise updated successfully");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteExercise(int id) 
		{
			var deleteexecise = await _exerciseSurvice.DeleteExerciseAsync(id);
			if (!deleteexecise)
				return NotFound("Exercise not found.");

			return Ok("Exercise deleted successfully");
		}
	}
}
