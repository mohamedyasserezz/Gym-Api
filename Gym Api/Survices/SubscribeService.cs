using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Repo;

namespace Gym_Api.Survices
{
	public class SubscribeService : ISubscribeService
	{
		private readonly ISubscribeRepository _repository;
		private readonly IFileService _fileService;

		public SubscribeService(ISubscribeRepository repository, IFileService fileService)
		{
			_repository = repository;
			_fileService = fileService;
		}

		public async Task<List<Subscribe>> GetAllSubscriptionsAsyncS()
		{
			return await _repository.GetAllSubscriptionsAsync();
		}


		public async Task<Subscribe> GetSubscribeByIdS(int id)
		{
			return await _repository.GetSubscribeById(id);
		}


		public async Task<string> CreateSubscriptionAsync(CreateSubscriptionDto dto)
		{
			// تحقق إذا المستخدم مشترك بالفعل
			var hasSubscription = await _repository.HasActiveSubscriptionAsync(dto.User_ID, dto.Coach_ID);
			if (hasSubscription)
				return "أنت مشترك بالفعل مع هذا الكوتش.";

			// حفظ صورة إثبات الدفع
			var proofPath = await _fileService.SaveFileAsync(dto.PaymentProof, "PaymentProofs");

			// إنشاء الاشتراك الجديد
			var subscription = new Subscribe
			{
				User_ID = dto.User_ID,
				Coach_ID = dto.Coach_ID,
				SubscriptionType = dto.SubscriptionType,
				PaymentProof = proofPath,
				IsPaid = true,
				IsApproved = false,
				StartDate = DateTime.UtcNow,
				EndDate = CalculateEndDate(dto.SubscriptionType)
			};

			// ميثود صغيرة نحسب بيها تاريخ الانتهاء حسب النوع
			  DateTime CalculateEndDate(string subscriptionType)
			{
				return subscriptionType switch
				{
					"شهر" => DateTime.UtcNow.AddMonths(1),
					"3 شهور" => DateTime.UtcNow.AddMonths(3),
					"6 شهور" => DateTime.UtcNow.AddMonths(6),
					"سنه" => DateTime.UtcNow.AddMonths(12),
					_ => DateTime.UtcNow.AddMonths(1) // الديفولت شهر لو مش اختار حاجة
				};
			}
			await _repository.AddSubscriptionAsync(subscription);
			return "تم إنشاء الاشتراك بنجاح، في انتظار موافقة الإدارة.";
		}


		public async Task<List<Subscribe>> GetPendingSubscriptionsAsync()
		{
			return await _repository.GetPendingSubscriptionsAsync();
		}


		public async Task<bool> ApproveSubscriptionAsync(int subscribeId)
		{
			var subscribe = await _repository.GetSubscribeById(subscribeId);
			if (subscribe == null || !subscribe.IsPaid || subscribe.IsApproved)
			{
				return false;
			}
			subscribe.IsApproved = true;
			subscribe.Status = "Active";
			await _repository.ApproveSubscriptionAsync(subscribe);
			return true;
		}

		public async Task<bool> RejectSubscriptionAsync(int subscribeId)
		{
			var subscribe = await _repository.GetSubscribeById(subscribeId);
			if (subscribe == null || !subscribe.IsPaid || subscribe.IsApproved)
			{
				return false;
			}
			subscribe.Status = "Rejected";
			subscribe.IsApproved = false;
			await _repository.RejectSubscriptionAsync(subscribe);
			 return true;
		}


		public async Task<List<UserSubscriptionDto>> GetUsersSubscribedToCoachAsync(int coachId)
		{
			return await _repository.GetUsersSubscribedToCoachAsync(coachId);
		}

		public async Task<List<CoachSubscriptionDto>> GetUserSubscriptionsAsync(int userId)
		{
			return await _repository.GetUserSubscriptionsAsync(userId);
		}

	}
}
