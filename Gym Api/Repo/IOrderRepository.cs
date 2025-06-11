using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface IOrderRepository
	{
		public Task<List<Order>> GetAllOrders();
		public Task<List<Order>> GetAllUserOrders(string UserId);
		public Task<Order?> GetOrderByIdR(int id);
		public Task<Product?> GetProductByIdAsync(int id);
		public Task<Order> AddOrderR(Order order);
		public Task<bool> UpdateProductAsync(Product product);
		public Task<List<Order>> GetAllPendingOrdersAsync();
		public Task<bool> AcceptOrderR(Order order);
		public Task<bool> RejectOrderR(Order order);
		public Task<bool> UpdateOrder(Order order);	
	}
}
