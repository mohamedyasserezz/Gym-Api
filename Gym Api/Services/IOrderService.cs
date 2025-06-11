using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Services
{
	public interface IOrderService
	{
		public Task<List<Order>> GetAllOrdersAsync();
		public Task<List<Order>> GetAllUserOrdersAsync(string UserId);
		public Task<object> GetOrderByIdAsync(int id);
		public Task<object> AddOrderAsync(CreateOrderDto createOrderDto);
		public Task<List<Order>> GetAllPendingOrdersAsync();
		public Task<bool> AcceptOrderAsync(int Orderid);
		public Task<bool> RejectOrderAsync(int orderid);
		public Task<bool> UpdateOrderAsync(int OrderId, UpdateOrderDto updateOrderDto);
	}
}
