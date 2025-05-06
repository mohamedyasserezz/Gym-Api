using Gym_Api.DTO;
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



		[HttpGet("GetAllProducts")]
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await _productService.GetAllProductsAsyncS();
			return Ok(products);
		}



		[HttpGet("GetProductById{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var product = await _productService.GetProductByIdAsyncS(id);
			if (product == null)
				return NotFound($"product with id {id} not found");
			return Ok(product);
		}


		[HttpGet("Getproductscount")]
		public async Task<IActionResult> Getproductscount()
		{
			var products = await _productService.Getproductscountasync();
			return Ok(products);
		}


		[HttpPost("AddNewProduct")]
		public async Task<IActionResult> AddProduct([FromForm] CreateProductDto dto)
		{
			var product = await _productService.AddProductAsyncS(dto);
			return CreatedAtAction(nameof(GetProductById), new { id = product.Product_ID }, product);
		}



		[HttpPut("UpdateProdcut{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto dto)
		{
			var result = await _productService.UpdateProductAsyncS(id, dto);
			if (!result)
				return NotFound($"Product with id {id} not found");
			return Ok("Product updated successfully.");
		}



		[HttpDelete("DeleteProduct{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var result = await _productService.DeleteProductAsyncS(id);
			if (!result)
				return NotFound($"Product with id {id} not found");
			return Ok("Product deleted successfully.");
		}
	}
}
