using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class CoachRepository : ICoachRepository
	{
		private readonly AppDbContext _context;

		public CoachRepository(AppDbContext context)
		{
			_context = context;
		}


		public async Task<List<Coach>> GetAllAsyncR()
		{
			return await _context.Coaches.Where(c => c.IsApproved).ToListAsync();
		}



		public async Task<Coach?> GetByIdAsyncR(int id)
		{
			return await _context.Coaches.FirstOrDefaultAsync(c => c.Coach_ID == id && c.IsApproved);
		}



		public async Task<List<Coach>?> GetBySpecializationAsyncR(string specialization)
		{
			return await _context.Coaches
				.Where(c => c.Specialization == specialization&& c.IsApproved)
				.ToListAsync();
		}

		public async Task<List<Coach>> GetApprovedCoachesAsyncR()
		{
			return await _context.Coaches.Where(c => c.IsApproved).ToListAsync();
		}

		public async Task<List<Coach>> GetUnapprovedCoachesAsyncR()
		{
			return await _context.Coaches.Where(c => !c.IsApproved).ToListAsync();
		}

		public async Task<bool> ApproveCoachAsyncR(Coach coach)
		{
			_context.Coaches.Update(coach);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
