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



		// ✅ Get All Exercises
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var exercises = await _exerciseSurvice.GetAllExercisesAsync();
			return Ok(exercises);
		}



		// ✅ Get by Name
		[HttpGet("{name}")]
		public async Task<IActionResult> GetByName(string name)
		{
			var exercise = await _exerciseSurvice.GetExerciseByNameAsync(name);
			if (exercise == null)
				return NotFound($"Exercise '{name}' not found.");

			return Ok(exercise);
		}



		// ✅ Add New Exercise
		[HttpPost]
		public async Task<IActionResult> AddExercise([FromBody] CreateNewExerciseDto dto)
		{
			var newExercise = await _exerciseSurvice.AddExerciseAsync(dto);
			return CreatedAtAction(nameof(GetByName), new { name = newExercise.Exercise_Name }, newExercise);
		}


	}
}
