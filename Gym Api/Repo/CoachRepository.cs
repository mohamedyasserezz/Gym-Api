using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
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


		public async Task<List<CoachList>> GetAllAsyncR()
		{
			return await _context.Coaches.Include(c => c.ApplicationUser).Select(c => new CoachList
			{
				UserId = c.UserId,
				FullName = c.ApplicationUser.FullName,
				Image = c.ApplicationUser.Image,
				Email = c.ApplicationUser.Email,
				Specialization = c.Specialization,
				Portfolio_Link = c.Portfolio_Link,
				Experience_Years = c.Experience_Years,
				Availability = c.Availability,
				Bio =  c.Bio,
				IsConfirmedByAdmin = c.IsConfirmedByAdmin
			})
			.ToListAsync();
		}



		public async Task<Coach?> GetByIdAsyncR(string id)
		{
			return await _context.Coaches.Include(c => c.ApplicationUser)
			.FirstOrDefaultAsync(c => c.UserId == id);
		}



		public async Task<List<CoachList>?> GetBySpecializationAsyncR(string specialization)
		{
			return await _context.Coaches.Include(c => c.ApplicationUser).Select(c => new CoachList
			{
				UserId = c.UserId,
				FullName = c.ApplicationUser.FullName,
				Image = c.ApplicationUser.Image,
				Email = c.ApplicationUser.Email,
				Specialization = c.Specialization,
				Portfolio_Link = c.Portfolio_Link,
				Experience_Years = c.Experience_Years,
				Availability = c.Availability,
				Bio = c.Bio,
				IsConfirmedByAdmin = c.IsConfirmedByAdmin
			})
				.Where(c => c.Specialization == specialization&& c.IsConfirmedByAdmin)
				.ToListAsync();
		}

		public async Task<List<CoachList>> GetApprovedCoachesAsyncR()
		{
			return await _context.Coaches.Include(c => c.ApplicationUser).Select(c => new CoachList
			{
				UserId = c.UserId,
				FullName = c.ApplicationUser.FullName,
				Image = c.ApplicationUser.Image,
				Email = c.ApplicationUser.Email,
				Specialization = c.Specialization,
				Portfolio_Link = c.Portfolio_Link,
				Experience_Years = c.Experience_Years,
				Availability = c.Availability,
				Bio = c.Bio,
				IsConfirmedByAdmin = c.IsConfirmedByAdmin
			})
			.Where(c => c.IsConfirmedByAdmin).ToListAsync();
		}

		public async Task<List<CoachList>> GetUnapprovedCoachesAsyncR()
		{
			return await _context.Coaches.Include(c => c.ApplicationUser).Select(c => new CoachList
			{
				UserId = c.UserId,
				FullName = c.ApplicationUser.FullName,
				Image = c.ApplicationUser.Image,
				Email = c.ApplicationUser.Email,
				Specialization = c.Specialization,
				Portfolio_Link = c.Portfolio_Link,
				Experience_Years = c.Experience_Years,
				Availability = c.Availability,
				Bio = c.Bio,
				IsConfirmedByAdmin = c.IsConfirmedByAdmin
			})
				.Where(c => !c.IsConfirmedByAdmin).ToListAsync();
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


		public async Task<bool> UpdateAsync(Coach coach)
		{
			_context.Coaches.Update(coach);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(Coach coach)
		{
			_context.Coaches.Remove(coach);
			await _context.SaveChangesAsync();
			return true;
		}





	}
}
