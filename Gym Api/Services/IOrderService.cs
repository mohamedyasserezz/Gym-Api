using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Services
{
	public interface IOrderService
	{
		public Task<Order?> GetOrderByIdAsync(int id);
		public Task<object> AddOrderAsync(CreateOrderDto createOrderDto);
	}
}
