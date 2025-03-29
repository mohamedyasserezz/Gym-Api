using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface IExerciseSurvice
	{
		public Task<List<Exercise>> GetAllExercisesAsync();
		public Task<Exercise?> GetExercisesByNameAsync(string name);
		public Task<Exercise> AddNewExerciseAsync(CreateNewExerciseDto createNewExerciseDto);
		public Task<bool> UpdateExerciseAsync(int id,UpdateExerciseDto exerciseDto);
		public Task<bool> DeleteExerciseAsync(int id);
	}
}
