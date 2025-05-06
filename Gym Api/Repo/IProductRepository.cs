using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface IProductRepository
	{
	    public Task<List<Product>> GetAllProductsAsync();
		public Task<Product?> GetProductByIdAsync(int id);
		public Task<int> GetProductsCount();
		public Task<Product> AddProductAsync(Product product);
		public Task<bool> UpdateProductAsync(Product product);
	    public Task<bool> DeleteProductAsync(Product product);
	}
}
