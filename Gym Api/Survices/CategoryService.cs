using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.Repo;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<List<Category>> GetAllCategoriesAsync() 
		{
			return await _categoryRepository.GetAllCategoriesAsyncR();
		}


		public async Task<Category?> GetCategoryByNameAsync(string categoryName)
		{
			return await _categoryRepository.GetCategoryByNameAsyncR(categoryName);
		}

	}
}
