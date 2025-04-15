using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface ICategoryRepository
	{
		public Task<List<Category>> GetAllCategoriesAsyncR();
		public Task<Category?> GetCategoryByNameAsyncR(string categoryName);
	}
}
