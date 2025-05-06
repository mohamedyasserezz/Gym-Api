using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class CoachRepository : ICoachRepository
	{
		private readonly ApplicationDbContext _context;

		public CoachRepository(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<List<Coach>> GetAllAsyncR()
		{
			return await _context.Coaches.ToListAsync();
		}



		public async Task<Coach?> GetByIdAsyncR(string id)
		{
			return await _context.Coaches.FirstOrDefaultAsync(c => c.UserId == id && c.IsConfirmedByAdmin);
		}



		public async Task<List<Coach>?> GetBySpecializationAsyncR(string specialization)
		{
			return await _context.Coaches
				.Where(c => c.Specialization == specialization&& c.IsConfirmedByAdmin)
				.ToListAsync();
		}

		public async Task<List<Coach>> GetApprovedCoachesAsyncR()
		{
			return await _context.Coaches.Where(c => c.IsConfirmedByAdmin).ToListAsync();
		}

		public async Task<List<Coach>> GetUnapprovedCoachesAsyncR()
		{
			return await _context.Coaches.Where(c => !c.IsConfirmedByAdmin).ToListAsync();
		}		

		public async Task<int> GetCoachCount()
		{
			return await _context.Coaches.CountAsync();
		}
		public async Task<bool> ApproveCoachAsyncR(Coach coach)
		{
			_context.Coaches.Update(coach);
			await _context.SaveChangesAsync();
			return true;
		}

	}
}
