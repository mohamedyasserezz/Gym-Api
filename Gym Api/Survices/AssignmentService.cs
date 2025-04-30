using Gym_Api.Data;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Survices
{
	public class AssignmentService : IAssignmentService
	{
		private readonly IAssignmentRepository _repository;
		public AssignmentService(IAssignmentRepository repository)
		{
			_repository = repository;
		}

		public async Task<string> AddAssignmentAsync(CreateAssignmentDto dto)
		{
			// تحقق من وجود اشتراك ساري
			var hasSubscription = await _repository.HasActiveSubscriptionAsync(dto.UserId, dto.CoachId);
			if (hasSubscription)
			{
				// إنشاء المهمة
				var assignment = new Assignment
				{
					User_ID = dto.UserId,
					Coach_ID = dto.CoachId,
					Exercise_ID = dto.ExerciseId,
					Day = dto.Day,
					Notes = dto.Notes
				};
				await _repository.AddAssignmentAsync(assignment);
				return "تم إضافة المهمة بنجاح للمشترك";
			}

			// إضافة مسار عودة في حالة عدم وجود اشتراك ساري
			return "لا يمكن إضافة المهمة، لا يوجد اشتراك ساري للمستخدم";
		}

		public async Task<List<Assignment>> GetUserAssignmentsByDayAsync(int userId, string day)
		{
			return await _repository.GetUserAssignmentsByDayAsync(userId, day);
		}
	}
}
