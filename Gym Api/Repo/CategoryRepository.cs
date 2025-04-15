using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly AppDbContext _context;
		public CategoryRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Category>> GetAllCategoriesAsyncR()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category?> GetCategoryByNameAsyncR(string categoryName)
		{
			return await _context.Categories
				.FirstOrDefaultAsync(c => c.Category_Name == categoryName);
		}
	}
}
