using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface IOrderRepository
	{
		public Task<Order?> GetOrderByIdR(int id);
		public Task<Product?> GetProductByIdAsync(int id);
		public Task<Order> AddOrderR(Order order);
		public Task<bool> UpdateProductAsync(Product product);
	}
}
