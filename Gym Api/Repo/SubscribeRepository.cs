using Gym_Api.Data.Models;
using Gym_Api.Data;
using Microsoft.EntityFrameworkCore;
using Gym_Api.DTO;

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

		public async Task<List<UserSubscriptionDto>> GetUsersSubscribedToCoachAsync(int coachId)
		{
			return await _context.Subscriptions
	      .Where(s => s.Coach_ID == coachId && s.IsPaid && s.IsApproved && s.EndDate > DateTime.UtcNow)
	         .Select(s => new UserSubscriptionDto
	        {
		   UserId = s.User.User_ID,
		   UserName = s.User.Name,
		   UserEmail = s.User.Email,
		   Image = s.User.ProfileImageUrl

	              })
			 
	            .ToListAsync();
		}

		public async Task<List<CoachSubscriptionDto>> GetUserSubscriptionsAsync(int userId)
		{
			return await _context.Subscriptions
				.Where(s => s.User_ID == userId && s.IsPaid && s.IsApproved && s.EndDate > DateTime.UtcNow)
				.Select(s => new CoachSubscriptionDto
				{
					CoachId = s.Coach.Coach_ID,
					CoachName = s.Coach.Name,
					Specialization = s.Coach.Specialization,
					Image = s.Coach.ImageUrl
				})
				.ToListAsync();
		}




	}
}
