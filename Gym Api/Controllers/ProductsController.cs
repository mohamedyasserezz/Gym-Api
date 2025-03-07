using Gym_Api.Contract;
using Gym_Api.Data.Models;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await _productService.GetAllProductsAsync();
			return Ok(products);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null) return NotFound($"Product with ID {id} not found");
			return Ok(product);
		}


		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
		{
			var newProduct = await _productService.CreateProductAsync(productDTO);
			return Ok(newProduct);
		}



		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product updateproduct)
		{
			var result = await _productService.UpdateProductAsync(id, updateproduct);
			if (!result)
			{
				return NotFound($"Product with ID {id} not found");
			}
			return NoContent();
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct([FromRoute] int id)
		{
			var result = await _productService.DeleteProductAsync(id);
			if (!result)
			{ 
			return NotFound($"Product with ID {id} not found");
		    }
			return NoContent();
		}


	}
}
