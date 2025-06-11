using Gym_Api.Data.Models;
using Gym_Api.Data;
using Microsoft.EntityFrameworkCore;
using Gym_Api.DTO;
using Org.BouncyCastle.Crypto.Macs;

namespace Gym_Api.Repo
{
	public class AssignmentRepository : IAssignmentRepository
	{
		private readonly ApplicationDbContext _context;
		
		public AssignmentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Assignment?> GetByIdAsyncR(int id)
		{
			return await _context.Assignments.FirstOrDefaultAsync(a => a.Assignment_ID == id);
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

		public async Task<List<Assignment>> GetUserAssignmentsByDayAsync(string userId, DateTime day)
		{
			return await _context.Assignments
		.Where(a => a.User_ID == userId && a.Day == day)
		.Include(a => a.AssignmentExercises).ThenInclude(e => e.Exercise)
		.ToListAsync();
		}


		public async Task<List<AssignmentViewDto>> GetAllUserAssignmentsAsync(string userId)
		{
			var assignments = await _context.Assignments
			.Where(a => a.User_ID == userId)
			.Include(a => a.AssignmentExercises)
				.ThenInclude(ae => ae.Exercise)
			.ToListAsync();

			var assignmentDtos = assignments.Select(a => new AssignmentViewDto
			{
				Assignment_ID = a.Assignment_ID,
				Day = a.Day,
				Notes = a.Notes,
				IsCompleted = a.IsCompleted,
				Exercises = a.AssignmentExercises.Select(ae => new ExerciseDto
				{
					Exercise_ID = ae.Exercise.Exercise_ID,
					Exercise_Name = ae.Exercise.Exercise_Name,
					Description = ae.Exercise.Description,
					Image_url = ae.Exercise.Image_url,
					Image_gif = ae.Exercise.Image_gif,
					Duration = ae.Exercise.Duration,
					Target_Muscle = ae.Exercise.Target_Muscle,
					Difficulty_Level = ae.Exercise.Difficulty_Level,
					Calories_Burned = ae.Exercise.Calories_Burned,
					Category_ID = ae.Exercise.Category_ID
				}).ToList()
			}).ToList();

			return assignmentDtos;
		}


		public async Task<bool> CompleteAssignmetAsyncR(Assignment assignment)
		{
			_context.Assignments.Update(assignment);
			await _context.SaveChangesAsync();
			return true;
		}



	}
}
