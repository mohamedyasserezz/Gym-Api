using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class ExerciseSurvice : IExerciseSurvice
	{
		private readonly IExerciseRepository _repository;
		private readonly IFileService _fileService;
		public ExerciseSurvice(IExerciseRepository repository, IFileService fileService)
		{
			_repository = repository;
			_fileService = fileService;	
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
			var exercise = new Exercise()
			{
				Exercise_Name = dto.Exercise_Name,
				Description = dto.Description,
				Duration = dto.Duration,
				Target_Muscle = dto.Target_Muscle,
				Difficulty_Level = dto.Difficulty_Level,
				Calories_Burned = dto.Calories_Burned,
				Category_ID = dto.Category_ID
			};
			if (dto.Image_url is not null)
			{
				exercise.Image_url = await _fileService.SaveFileAsync(dto.Image_url, "Exercise");
			}
			if(dto.Image_gif is not null)
			{
				exercise.Image_gif = await _fileService.SaveFileAsync(dto.Image_gif,"Exercise");
			}
			await _repository.AddExerciseAsyncR(exercise);
			return exercise;
		}

	}
}
