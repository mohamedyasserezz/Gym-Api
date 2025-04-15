using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface IExerciseRepository
	{
		public Task<List<Exercise>> GetAllExercisesAsyncR();
		public Task<Exercise?> GetExerciseByNameAsyncR(string name);
		public Task<Exercise> AddExerciseAsyncR(Exercise exercise);
	}
}
