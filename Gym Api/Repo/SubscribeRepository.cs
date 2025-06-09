using Gym_Api.Data.Models;
using Gym_Api.Data;
using Microsoft.EntityFrameworkCore;
using Gym_Api.DTO;

namespace Gym_Api.Repo
{
	public class SubscribeRepository : ISubscribeRepository
	{
		private readonly ApplicationDbContext _context;
		public SubscribeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Subscribe>> GetAllSubscriptionsAsync()
		{
			return await _context.Subscriptions.ToListAsync();
		}

		public async Task<Subscribe> GetSubscribeById(int id)
		{
			return await _context.Subscriptions.Include(s => s.User).ThenInclude(s => s.ApplicationUser).FirstOrDefaultAsync(s => s.Subscribe_ID == id);
		}

		public async Task<List<Subscribe>> GetSubscribeByUserIdAsyncR(string userId)
		{
			return await _context.Subscriptions
				.Include(s => s.User)
					.ThenInclude(u => u.ApplicationUser)
				.Where(s => s.User_ID == userId)
				.ToListAsync();
		}


		public async Task<bool> HasActiveSubscriptionAsync(string userId, string coachId)
		{
			return await _context.Subscriptions.AnyAsync(s =>
				s.User_ID == userId &&
				s.Coach_ID == coachId &&
				s.IsPaid == true &&
				s.Status == "Active" &&
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
				.Where(s => s.Status == "pending")
				.Include(s => s.User)
				.ToListAsync();
		}

		public async Task<List<Subscribe>> GetRejectedSubscriptionsAsync()
		{
			return await _context.Subscriptions
				.Where(s => s.Status == "Rejected")
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
			_context.Subscriptions.Update(subscribe);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<List<UserSubscriptionDto>> GetUsersSubscribedToCoachAsync(string coachId)
		{
			return await _context.Subscriptions
	      .Where(s => s.Coach_ID == coachId && s.IsPaid && s.IsApproved && s.EndDate > DateTime.UtcNow)
	         .Select(s => new UserSubscriptionDto
	        {
		   UserId = s.User.UserId,
		   UserName = s.User.ApplicationUser.FullName,
		   UserEmail = s.User.ApplicationUser.Email!,
		   Image = s.User.ApplicationUser.Image!,
		   Height = s.User.Height,
		   Weight =s.User.Weight,
		   BDate =s.User.BDate,
		   Gender =s.User.Gender,
		   MedicalConditions =s.User.MedicalConditions,
		   Allergies =s.User.Allergies,
		   Fitness_Goal =s.User.Fitness_Goal,
		   Subscribe_ID = s.Subscribe_ID,
		   StartDate = s.StartDate,
		   EndDate = s.EndDate,
		   Status = s.Status,
		   SubscriptionType = s.SubscriptionType,
		   IsApproved = s.IsApproved,
		   IsPaid = s.IsPaid
	              })
	            .ToListAsync();
		}

		public async Task<List<CoachSubscriptionDto>> GetUserSubscriptionsAsync(string userId)
		{
			return await _context.Subscriptions
				.Where(s => s.User_ID == userId)
				.Select(s => new CoachSubscriptionDto
				{
					CoachId = s.Coach.UserId,
					CoachName = s.Coach.ApplicationUser.FullName,
					Specialization = s.Coach.Specialization,
					Image = s.Coach.ApplicationUser.Image!
				})
				.ToListAsync();
		}




	}
}
