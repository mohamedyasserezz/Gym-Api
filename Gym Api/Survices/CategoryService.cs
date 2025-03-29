using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class CategoryService : ICategoryService
	{
		private readonly AppDbContext _context;
		public CategoryService(AppDbContext context) 
		{
			_context = context;
		}

		public async Task<List<Category>> GetAllCategoriesAsync() 
		{
			return await _context.Categories.ToListAsync();
		}


		public async Task<Category?> GetCategoriesByNameAsync(string name) 
		{
			return await _context.Categories.FirstOrDefaultAsync(c => c.Category_Name == name);
		}


		public async Task<Category> AddCategoryAsync(Category category) 
		{
			_context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
			return category;
		}


		public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto updatedCategory)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
				return false;

			category.Category_Name = updatedCategory.Category_Name;
			category.ImageUrl = updatedCategory.ImageUrl; 
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<bool> DeleteCategoryAsync(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
				return false;

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
