using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class ExerciseRepository : IExerciseRepository
	{
		private readonly AppDbContext _context;
		public ExerciseRepository(AppDbContext context)
		{
			_context = context;
		}


		public async Task<List<Exercise>> GetAllExercisesAsyncR()
		{
			return await _context.Exercises.ToListAsync();
		}



		public async Task<Exercise?> GetExerciseByNameAsyncR(string name)
		{
			return await _context.Exercises
				.FirstOrDefaultAsync(e => e.Exercise_Name == name);
		}



		public async Task<Exercise> AddExerciseAsyncR(Exercise exercise)
		{
			await _context.Exercises.AddAsync(exercise);
			await _context.SaveChangesAsync();
			return exercise;
		}


	}
}
