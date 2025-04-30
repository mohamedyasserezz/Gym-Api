using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IFileService _fileService;

		public CategoryService(ICategoryRepository categoryRepository,IFileService fileService)
		{
			_categoryRepository = categoryRepository;
			_fileService = fileService;
		}

		


		public async Task<List<Category>> GetAllCategoriesAsync() 
		{
			return await _categoryRepository.GetAllCategoriesAsyncR();
		}

		public async Task<Category?> GetCategoryByIdAsync(int id)
		{
			return await _categoryRepository.GetCategoryById(id);
		}

		public async Task<Category?> GetCategoryByNameAsync(string categoryName)
		{
			return await _categoryRepository.GetCategoryByNameAsyncR(categoryName);
		}

		public async Task<Category> AddNewCategoryAsync(Addnewcategory addnewcategory)
		{

			var category = new Category()
			{
				Category_Name = addnewcategory.CategoryName
			};
			if(addnewcategory.CategoryImage is not null)
			{
				category.ImageUrl = await _fileService.SaveFileAsync(addnewcategory.CategoryImage, "category"); 
			}
			await _categoryRepository.AddNewCategory(category);
			return category;
		}


		public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
		{
			var category = await _categoryRepository.GetCategoryById(id);
			if(category == null)
			{
				return false;
			}
			category.Category_Name = dto.CategoryName;

			if (dto.CategoryImage is not null)
			{
				category.ImageUrl = await _fileService.SaveFileAsync(dto.CategoryImage, "category");
			}

			await _categoryRepository.UpdateCategoryAsyncR(category);
			return true;

		}



		public async Task<bool> DeleteCategoryAsync(int id)
		{
			var category = await _categoryRepository.GetCategoryById(id);
			if( category == null)
			{
				return false;
			}
			await _categoryRepository.DeleteCategoryAsyncR(category);
			return true;
		}


	}
}
