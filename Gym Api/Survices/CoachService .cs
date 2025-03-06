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
		public async Task<Coach?> GetCoachByIdAsync(int id)
		{
			return await _context.Coaches.FindAsync(id);
		}
		public async Task<Coach> CreateCoachAsync(Coach coach)
		{
			_context.Coaches.Add(coach);
			await _context.SaveChangesAsync();
			return coach;
		}
		public async Task<bool> UpdateCoachAsync(int id, Coach updatedCoach)
		{
			var coach = await _context.Coaches.FindAsync(id);
			if (coach == null) return false;

			coach.Fname = updatedCoach.Fname;
			coach.Lname = updatedCoach.Lname;
			coach.Email = updatedCoach.Email;
			coach.Password = updatedCoach.Password;
			coach.Portfolio_Link= updatedCoach.Portfolio_Link;
			coach.Availability = updatedCoach.Availability;
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteCoachAsync(int id)
		{
			var coach = await _context.Coaches.FindAsync(id);
			if (coach == null) return false;

			_context.Coaches.Remove(coach);
			await _context.SaveChangesAsync();
			return true;
		}


	}
}
