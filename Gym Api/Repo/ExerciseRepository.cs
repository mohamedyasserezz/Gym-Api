using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Migrations;
using Gym_Api.Survices;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class ExerciseRepository : IExerciseRepository
	{
		private readonly ApplicationDbContext _context;
		public ExerciseRepository(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<List<Exercise>> GetAllExercisesAsyncR()
		{
			return await _context.Exercises.ToListAsync();
		}

		public async Task<Exercise?> GetExerciseById(int id)
		{
			return await _context.Exercises.FirstOrDefaultAsync(e => e.Exercise_ID == id);
		}


		public async Task<Exercise?> GetExerciseByNameAsyncR(string name)
		{
			return await _context.Exercises
				.FirstOrDefaultAsync(e => e.Exercise_Name == name);
		}

		public async Task<List<Exercise>> GetByCategoryIdAsync(int CategoryId)
		{
			return await _context.Exercises.Where(e => e.Category_ID == CategoryId).ToListAsync();
		}


		public async Task<Exercise> AddExerciseAsyncR(Exercise exercise)
		{
			await _context.Exercises.AddAsync(exercise);
			await _context.SaveChangesAsync();
			return exercise;
		}

		public async Task<bool> UpdateExerciseAsyncR(Exercise exercise)
		{
			_context.Exercises.Update(exercise);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteExerciseAsyncR(Exercise exercise)
		{
			_context.Exercises.Remove(exercise);
			await _context.SaveChangesAsync();
			return true;
		}




	}
}
