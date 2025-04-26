using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Survices;
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

		public async Task<Category?> GetCategoryById(int Id)
		{
			return await _context.Categories.FirstOrDefaultAsync(c => c.Category_ID == Id);
		}

		public async Task<Category?> GetCategoryByNameAsyncR(string categoryName)
		{
			return await _context.Categories
				.FirstOrDefaultAsync(c => c.Category_Name == categoryName);
		}

		public async Task<Category> AddNewCategory(Category category)
		{
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();	
			return category;
		}

		public async Task<bool> UpdateCategoryAsyncR(Category category)
		{
			_context.Categories.Update(category);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteCategoryAsyncR(Category category)
		{
      		_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
