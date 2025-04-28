using Gym_Api.Data.Models;
using Gym_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class SubscribeRepository : ISubscribeRepository
	{
		private readonly AppDbContext _context;
		public SubscribeRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Subscribe>> GetAllSubscriptionsAsync()
		{
			return await _context.Subscriptions.ToListAsync();
		}

		public async Task<Subscribe> GetSubscribeById(int id)
		{
			return await _context.Subscriptions.FirstOrDefaultAsync(s => s.Subscribe_ID == id);
		}


		public async Task<bool> HasActiveSubscriptionAsync(int userId, int coachId)
		{
			return await _context.Subscriptions.AnyAsync(s =>
				s.User_ID == userId &&
				s.Coach_ID == coachId &&
				s.IsPaid == true &&
				s.IsApproved == true &&
				s.EndDate > DateTime.UtcNow);
		}

		public async Task<Subscribe> AddSubscriptionAsync(Subscribe subscribe)
		{
			await _context.Subscriptions.AddAsync(subscribe);
			await _context.SaveChangesAsync();
			return subscribe;
		}


		public async Task<List<Subscribe>> GetPendingSubscriptionsAsync()
		{
			return await _context.Subscriptions
				.Where(s => s.IsPaid && !s.IsApproved)
				.Include(s => s.User)
				.ToListAsync();
		}

		public async Task<bool> ApproveSubscriptionAsync(Subscribe subscribe)
		{
			_context.Subscriptions.Update(subscribe);
			await _context.SaveChangesAsync();
			return true;
			
		}

		public async Task<bool> RejectSubscriptionAsync(Subscribe subscribe)
		{
			_context.Subscriptions.Remove(subscribe);
			await _context.SaveChangesAsync();
			return true;
		}






	}
}
