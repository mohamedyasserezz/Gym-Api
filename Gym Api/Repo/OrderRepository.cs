using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;
		public OrderRepository(ApplicationDbContext context)
		{
			_context = context;
		}



		public async Task<Order?> GetOrderByIdR(int id)
		{
			return await _context.Orders.FirstOrDefaultAsync(s => s.Order_id == id);
		}

		public async Task<Product?> GetProductByIdAsync(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(p => p.Product_ID == id);
		}

		public async Task<Order> AddOrderR(Order order)
		{
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();
			return order;
		}

		public async Task<bool> UpdateProductAsync(Product product)
		{
			_context.Products.Update(product);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
