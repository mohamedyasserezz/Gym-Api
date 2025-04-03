using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class CoachService : ICoachService
	{
		private readonly AppDbContext _context;
		public CoachService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Coach>> GetAllCoachesAsync()
		{
			return await _context.Coaches.ToListAsync();
		}

		public async Task<List<Coach>> GetAllApprovedCoachesAsync()
		{
			return await _context.Coaches.Where(c => c.IsApproved == true).ToListAsync();
		}

		public async Task<Coach> GetCoachByIdAsync(int CoachId)
		{
			return await _context.Coaches.FindAsync(CoachId);
		}

		public async Task<string> ApproveCoachAsync(int CoachId)
		{
			var coach = await _context.Coaches.FindAsync(CoachId);
			if (coach == null) 
			{
				return "Coach Not Exist";
			}
			if(coach.IsApproved)
			{
				return "This subscription is already approved";
			}
			coach.IsApproved = true;
			await _context.SaveChangesAsync();
			return "Approved successfully";
		}


		public async Task<string> RejectCoachAsync(int CoachId)
		{
			var coach = await _context.Coaches.FindAsync(CoachId);
			if (coach == null)
			{
				return "Coach Not Exist";
			}
			if (coach.IsApproved)
			{
				return "The coach cannot be refused after being approved";
			}

			// رفض الكوتش
			_context.Coaches.Remove(coach);
			await _context.SaveChangesAsync();
			return "Subscription was rejected and removed from the system";
		}


		public async Task<bool> DeleteCoachAsync(int coachId)
		{
			var coach = await _context.Coaches.FindAsync(coachId);
			if (coach == null) return false;

			_context.Coaches.Remove(coach);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
