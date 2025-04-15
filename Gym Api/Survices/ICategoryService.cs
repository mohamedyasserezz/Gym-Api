using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface ICategoryService
	{
		public Task<List<Category>> GetAllCategoriesAsync();
		public Task<Category?> GetCategoryByNameAsync(string categoryName);


	}
}
