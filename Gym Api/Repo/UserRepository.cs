using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
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

		public async Task<List<UserList>> GetAllUsers()
		{
			return await _context.Users
				   .Include(u => u.ApplicationUser)
				   .Select(u => new UserList
				   {
					   UserId = u.UserId,
					   FullName = u.ApplicationUser.FullName,
					   Image = u.ApplicationUser.Image,
					   Email = u.ApplicationUser.Email,
					   UserType = u.ApplicationUser.UserType,
					   Height = u.Height,
					   Weight = u.Weight,
					   BDate = u.BDate,
					   Gender = u.Gender,
					   MedicalConditions = u.MedicalConditions,
					   Allergies = u.Allergies,
					   Fitness_Goal = u.Fitness_Goal
				   })
				   .ToListAsync();
		}

		public async Task<User> GetUsersById(string id)
		{
			return await _context.Users.Include(u => u.ApplicationUser).FirstOrDefaultAsync(u => u.UserId == id);
		}

		public async Task<int> GetUserCount()
		{
			return await _context.Users.CountAsync();
		}

		public async Task<bool> UpdateUserAsync(User user)
		{
		    _context.Users.Update(user);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteUserAsync(User user)
		{
			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return true;
		}


	}
}
