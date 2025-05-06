using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Survices
{
	public interface IProductService
	{
		public Task<List<Product>> GetAllProductsAsyncS();
		public Task<Product?> GetProductByIdAsyncS(int id);
		public Task<int> Getproductscountasync();
		public Task<Product> AddProductAsyncS(CreateProductDto dto);
		public Task<bool> UpdateProductAsyncS(int id, UpdateProductDto dto);
		public Task<bool> DeleteProductAsyncS(int id);
	}
}
