using Gym_Api.Data.Models;
using Gym_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _context;
		public ProductRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Product>> GetAllProductsAsync()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product?> GetProductByIdAsync(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(p => p.Product_ID == id);
		}

		public async Task<Product> AddProductAsync(Product product)
		{
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			return product;
		}

		public async Task<bool> UpdateProductAsync(Product product)
		{
		    _context.Products.Update(product);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteProductAsync(Product product)
		{
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
