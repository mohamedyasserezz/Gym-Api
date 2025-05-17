using Gym_Api.Contract;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Migrations;

namespace Gym_Api.Survices
{
	public interface IExerciseSurvice
	{
		public Task<List<Exercise>> GetAllExercisesAsync();
		public Task<Exercise?> GetExerciseByIdAsync(int id);
		public Task<Exercise?> GetExerciseByNameAsync(string name);
		public Task<List<Exercise>> GetByCategoryIdAsync(int categoryId);
		public Task<Exercise> AddExerciseAsync(CreateNewExerciseDto dto);
		public Task<bool> UpdateExerciseAsync(int id, Updateexercise dto);
		public Task<bool> DeleteExerciseAsync(int id);


	}
}
