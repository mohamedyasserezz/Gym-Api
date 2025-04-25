using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface IExerciseSurvice
	{
		public Task<List<Exercise>> GetAllExercisesAsync();
		public Task<Exercise?> GetExerciseByNameAsync(string name);
		public Task<Exercise> AddExerciseAsync(CreateNewExerciseDto dto);
	}
}
