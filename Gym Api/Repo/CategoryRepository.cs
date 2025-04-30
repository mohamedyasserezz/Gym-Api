using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Survices;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IFileService _fileService;

		public CategoryRepository(ApplicationDbContext context, IFileService fileService)
		{
			_context = context;
			_fileService = fileService;
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

		public async Task<Category> AddNewCategory(Category category)
		{
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();	
			return category;
		}

		public async Task<bool> UpdateCategoryAsyncR(int id, UpdateCategoryDto dto)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(c => c.Category_ID == id);
			if (category == null)
				return false;

			category.Category_Name = dto.CategoryName;

			if (dto.CategoryImage is not null)
			{
				category.ImageUrl = await _fileService.SaveFileAsync(dto.CategoryImage, "category");
			}

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteCategoryAsyncR(int id)
		{
			var category = await _context.Categories.FirstOrDefaultAsync(c => c.Category_ID == id);
			if (category == null)
				return false;

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
