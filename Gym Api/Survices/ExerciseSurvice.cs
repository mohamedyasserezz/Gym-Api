using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.Repo;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class ExerciseSurvice : IExerciseSurvice
	{
		private readonly IExerciseRepository _repository;
		public ExerciseSurvice(IExerciseRepository repository)
		{
			_repository = repository;
		}



		public async Task<List<Exercise>> GetAllExercisesAsync()
		{
			return await _repository.GetAllExercisesAsyncR();
		}



		public async Task<Exercise?> GetExerciseByNameAsync(string name)
		{
			return await _repository.GetExerciseByNameAsyncR(name);
		}


		public async Task<Exercise> AddExerciseAsync(CreateNewExerciseDto dto)
		{
			var exercise = new Exercise
			{
				Exercise_Name = dto.Exercise_Name,
				Description = dto.Description,
				Image_url = dto.Image_url,
				Image_gif = dto.Image_gif,
				Duration = dto.Duration,
				Target_Muscle = dto.Target_Muscle,
				Difficulty_Level = dto.Difficulty_Level,
				Calories_Burned = dto.Calories_Burned,
				Category_ID = dto.Category_ID
			};

			return await _repository.AddExerciseAsyncR(exercise);
		}

	}
}
