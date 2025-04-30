using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface IAssignmentService
	{
	   public Task<Assignment> AddAssignmentAsync(CreateAssignmentDto assignmentDto);
		public Task<List<Assignment>> GetUserAssignmentsAsync(int userId);
		public Task<List<Assignment>> GetCoachAssignmentsAsync(int coachId);
		public Task<string> UpdateAssignmentAsync(int assignmentId, int coachId, UpdateAssignmentDto updateDto);
		public Task<string> CompleteAssignmentAsync(int UserId, int assignmentId);
		public Task<bool> DeleteAssignmentAsync(int assignmentId,int coachid);




	}
}
