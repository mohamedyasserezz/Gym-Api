using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class ProductService : IProductService
	{

		private readonly AppDbContext _context;
		public ProductService(AppDbContext context) 
		{
			_context = context;
		}


		public async Task<List<Product>> GetAllProductsAsync()
		{
			return await _context.Products.ToListAsync();
		}


		public async Task<Product?> GetProductByIdAsync(int id)
		{
			return await _context.Products.FindAsync(id);
		}


		public async Task<Product> CreateProductAsync(ProductDTO productDTO)
		{
			var product1 = new Product
			{
				Product_Name = productDTO.Product_Name,
				Description = productDTO.Description,
				Price = productDTO.Price,
			    Stock_Quantity = productDTO.Stock_Quantity,
				Discount = productDTO.Discount,
				Image_URL = productDTO.Image_URL
			};
			_context.Products.Add(product1);
			await _context.SaveChangesAsync();
			return product1;
		}


		public async Task<bool> UpdateProductAsync(int id, Product updateproduct)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null) return false;

			product.Product_Name = updateproduct.Product_Name;
			product.Description = updateproduct.Description;
			product.Price = updateproduct.Price;
			product.Stock_Quantity = updateproduct.Stock_Quantity;
			await _context.SaveChangesAsync();
			return true;
		}


		public async Task<bool> DeleteProductAsync(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null) return false;

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
			return true;
		}


	}
}
