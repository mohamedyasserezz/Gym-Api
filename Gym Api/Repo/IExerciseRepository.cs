using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Repo
{
	public interface IExerciseRepository
	{
		public Task<List<Exercise>> GetAllExercisesAsyncR();
		public Task<Exercise?> GetExerciseById(int id);
		public Task<Exercise?> GetExerciseByNameAsyncR(string name);
		public Task<List<Exercise>> GetByCategoryIdAsync(int categoryId);
		public Task<Exercise> AddExerciseAsyncR(Exercise exercise);
		public Task<bool> UpdateExerciseAsyncR(Exercise exercise);
		public Task<bool> DeleteExerciseAsyncR(Exercise exercise);
	}
}
