using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface ICategoryService
	{
		public Task<List<Category>> GetAllCategoriesAsync();
		public Task<Category?> GetCategoriesByNameAsync(string name);
		public Task<Category> AddCategoryAsync(Category category);
		public Task<bool> UpdateCategoryAsync(int id,UpdateCategoryDto categoryDto);
		public Task<bool> DeleteCategoryAsync(int id);

	}
}
