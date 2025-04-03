using Gym_Api.Contract;
using Gym_Api.Data;
using Gym_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class AssignmentService : IAssignmentService
	{
		private readonly AppDbContext _context;
		public AssignmentService(AppDbContext context) 
		{
			_context = context;
		}


		public async Task<Assignment> AddAssignmentAsync(CreateAssignmentDto createAssignmentDto)
		{
			var newAssignment = new Assignment
			{
				User_ID = createAssignmentDto.UserId,
				Coach_ID = createAssignmentDto.CoachId,
				Exercise_ID = createAssignmentDto.ExerciseId
			};
			_context.Assignments.Add(newAssignment);
			await _context.SaveChangesAsync();
			return newAssignment;
		}


		public async Task<List<Assignment>> GetUserAssignmentsAsync(int UserId)
		{
			var userassignment = await _context.Assignments
				.Where(u => u.User_ID == UserId)
				.Include(e => e.Exercise)
				.ToListAsync();
			return userassignment;
		}


		public async Task<List<Assignment>> GetCoachAssignmentsAsync(int coachId)
		{
			return await _context.Assignments
				.Where(a => a.Coach_ID == coachId)
				.Include(a => a.User)
				.ToListAsync();
		}


		public async Task<string> UpdateAssignmentAsync(int assignmentId, int coachId, UpdateAssignmentDto updateDto)
		{
			var assignment = await _context.Assignments
				.Include(a => a.Coach)
				.FirstOrDefaultAsync(a => a.Assignment_ID == assignmentId && a.Coach_ID == coachId);

			if (assignment == null)
				return "Assignment not found or you don't have permission to update it.";

			// تحديث بيانات التمرين
			assignment.Exercise_ID = updateDto.ExerciseId;
			await _context.SaveChangesAsync();
			return "Assignment updated successfully.";
		}


		public async Task<string> CompleteAssignmentAsync(int UserId,int assignmentId)
		{
			var assignment = await _context.Assignments.FirstOrDefaultAsync(u => u.User_ID == UserId && u.Assignment_ID == assignmentId);
			if (assignment == null)
			{
				return "Assignment not found.";
			}
			if (assignment.IsCompleted)
			{
				return "Assignment is already marked as completed.";
			}
			assignment.IsCompleted = true;
			await _context.SaveChangesAsync();
			return "Assignment marked as completed.";

		}

		public async Task<bool> DeleteAssignmentAsync(int assignmentId,int CoachId)
		{
			var assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Assignment_ID == assignmentId && a.Coach_ID == CoachId);
			if (assignment == null) return false;

			_context.Assignments.Remove(assignment);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
