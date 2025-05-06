using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Repo
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<User>> GetAllUsers()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<User> GetUsersById(string id)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
		}

		public async Task<int> GetUserCount()
		{
			return await _context.Users.CountAsync();
		}


	}
}
