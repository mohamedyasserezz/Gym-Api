using Gym_Api.Contract;
using Gym_Api.Data.Models;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategories() 
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}



		[HttpGet("{Categoryname}")]
		public async Task<IActionResult> GetCategoryByName(string Categoryname)
		{
			var category = await _categoryService.GetCategoriesByNameAsync(Categoryname);
			if (category == null) 
			{
				return NotFound($"This category {Categoryname} not exist");
			}
			return Ok(category);
		}


		[HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] Category category)
		{
			var newCategory = await _categoryService.AddCategoryAsync(category);
			return Ok(newCategory);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updatedCategory)
		{
			var result = await _categoryService.UpdateCategoryAsync(id, updatedCategory);
			if (!result)
				return NotFound("Category not found.");

			return Ok("Category updated successfully");
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var result = await _categoryService.DeleteCategoryAsync(id);
			if (!result)
				return NotFound("Category not found.");

			return Ok("Category deleted successfully.");
		}
	}
}
