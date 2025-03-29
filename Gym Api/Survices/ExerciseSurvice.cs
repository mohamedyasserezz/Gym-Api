using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class ExerciseSurvice : IExerciseSurvice
	{
		private readonly AppDbContext _context;
		public ExerciseSurvice(AppDbContext context) 
		{
			_context = context;
		}


		public async Task<List<Exercise>> GetAllExercisesAsync() 
		{
			return await _context.Exercises.ToListAsync();
		}

		public async Task<Exercise?> GetExercisesByNameAsync(string name) 
		{
			return await _context.Exercises.FirstOrDefaultAsync(c => c.Exercise_Name == name);
		}

		public async Task<Exercise> AddNewExerciseAsync(CreateNewExerciseDto createNewExerciseDto)  
		{
			var newexercise = new Exercise
			{
				Exercise_Name= createNewExerciseDto.Exercise_Name,
				Description= createNewExerciseDto.Description,
				Video_URL= createNewExerciseDto.Video_URL,
				Duration= createNewExerciseDto.Duration,
				Target_Muscle= createNewExerciseDto.Target_Muscle,
				Difficulty_Level= createNewExerciseDto.Difficulty_Level,
				Calories_Burned= createNewExerciseDto.Calories_Burned,
				Category_ID= createNewExerciseDto.Category_ID
			};
		    _context.Exercises.Add(newexercise);
			await _context.SaveChangesAsync();
			return newexercise;
		}

		public async Task<bool> UpdateExerciseAsync(int id,UpdateExerciseDto updateExerciseDto) 
		{
			var exercise = await _context.Exercises.FindAsync(id);
			if (exercise == null) 
			{
				return false;
			}
			exercise.Exercise_Name = updateExerciseDto.Exercise_Name;
			exercise.Description= updateExerciseDto.Description;
			exercise.Video_URL= updateExerciseDto.Video_URL;
			exercise.Target_Muscle= updateExerciseDto.Target_Muscle;
			exercise.Calories_Burned= updateExerciseDto.Calories_Burned;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteExerciseAsync(int id) 
		{
			var exercise = await _context.Exercises.FindAsync(id);
			if(exercise == null) 
			{
				return false;
			}
			_context.Exercises.Remove(exercise);
			await _context.SaveChangesAsync();
			return true;
		}


	}
}
