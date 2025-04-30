using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Microsoft.Identity.Client;

namespace Gym_Api.Repo
{
	public interface ICategoryRepository
	{
		public Task<List<Category>> GetAllCategoriesAsyncR();
		public Task<Category?> GetCategoryById(int Id);
		public Task<Category?> GetCategoryByNameAsyncR(string categoryName);
		public Task<Category> AddNewCategory(Category category);
		public Task<bool> UpdateCategoryAsyncR(Category category);
		public Task<bool> DeleteCategoryAsyncR(Category category);

	}
}
