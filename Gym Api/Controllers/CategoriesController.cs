using Gym_Api.Contract;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
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



		// ✅ Get All
		[HttpGet("GetAllCategories")]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}


		[HttpGet("GetCategoryById{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var category = await _categoryService.GetCategoryByIdAsync(id);
			if (category == null)
			{
				return NotFound($"category with id {id} not found");
			}
			return Ok(category);
		}


		// ✅ Get By Name
		[HttpGet("GetCategoryByName{name}")]
		public async Task<IActionResult> GetCategoryByName(string name)
		{
			var category = await _categoryService.GetCategoryByNameAsync(name);
			if (category == null)
				return NotFound($"Category '{name}' not found.");

			return Ok(category);
		}


		// AddNewCategory
		[HttpPost("AddNewCategory")]
		public async Task<IActionResult> AddNewCategory([FromForm]Addnewcategory addnewcategory)
		{
			var category = await _categoryService.AddNewCategoryAsync(addnewcategory);
			return Ok(category);
		}




		[HttpPut("Updatecategory{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateCategoryDto dto)
		{
			var result = await _categoryService.UpdateCategoryAsync(id, dto);
			if (!result)
				return NotFound("Category not found.");

			return Ok("Category updated successfully.");
		}



		[HttpDelete("Deletecategory{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var result = await _categoryService.DeleteCategoryAsync(id);
			if (!result)
				return NotFound("Category not found.");

			return Ok("Category deleted successfully.");
		}



	}
}
