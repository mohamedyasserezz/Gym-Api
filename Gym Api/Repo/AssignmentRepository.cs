using Gym_Api.Data.Models;
using Gym_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class AssignmentRepository : IAssignmentRepository
	{
		private readonly ApplicationDbContext _context;
		
		public AssignmentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<bool> HasActiveSubscriptionAsync(string userId, string coachId)
		{
			return await _context.Subscriptions.AnyAsync(s =>
				s.User_ID == userId &&
				s.Coach_ID == coachId &&
				s.IsPaid == true &&
				s.IsApproved == true &&
				s.EndDate > DateTime.UtcNow);
		}


		public async Task<Assignment> AddAssignmentAsync(Assignment assignment)
		{
			await _context.Assignments.AddAsync(assignment);
			await _context.SaveChangesAsync();
			return assignment;
		}

		public async Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, string day)
		{
			return await _context.Assignments
		.Where(a => a.User_ID.Trim().ToLower() == userId.Trim().ToLower() && a.Day.Trim() == day.Trim())
		.Include(a => a.Exercise)
		.ToListAsync();
		}


		public async Task<List<Assignment>> GetAllUserAssignmentsAsync(string userId)
		{
			return await _context.Assignments
				.Where(a => a.User_ID == userId)
				.Include(a => a.Exercise)
				.ToListAsync();
		}

	}
}
