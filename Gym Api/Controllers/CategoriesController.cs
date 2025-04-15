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



		// ✅ Get All
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}


		// ✅ Get By Name
		[HttpGet("{name}")]
		public async Task<IActionResult> GetCategoryByName(string name)
		{
			var category = await _categoryService.GetCategoryByNameAsync(name);
			if (category == null)
				return NotFound($"Category '{name}' not found.");

			return Ok(category);
		}

		
	}
}
