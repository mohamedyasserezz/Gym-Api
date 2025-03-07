using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface IProductService
	{
		Task<List<Product>> GetAllProductsAsync();
		Task<Product?> GetProductByIdAsync(int id);
		Task<Product> CreateProductAsync(ProductDTO productDTO);
		Task<bool> UpdateProductAsync(int id, Product updateproduct);
		Task<bool> DeleteProductAsync(int id);
	}
}
