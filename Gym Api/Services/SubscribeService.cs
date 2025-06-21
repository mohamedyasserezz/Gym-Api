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

		public async Task<List<Subscribe>> GetSubscribeByUseridAsync(string userid)
		{
			return await _repository.GetSubscribeByUserIdAsyncR(userid);
		}

		public async Task<Subscribe> CreateSubscriptionAsync(CreateSubscriptionDto dto)
		{
			if (dto.PaymentProof == null || dto.PaymentProof.Length == 0)
			{
				Console.WriteLine("PaymentProof is null or empty.");
				throw new ArgumentException("PaymentProof field is required and must contain a file.");
			}
			// تحقق إذا المستخدم مشترك بالفعل
			var hasSubscription = await _repository.HasActiveSubscriptionAsync(dto.User_ID, dto.Coach_ID);
			if (hasSubscription)
				return null;

			// حفظ صورة 
			var proofPath = await _fileService.SaveFileAsync(dto.PaymentProof, "PaymentProofs");

			// إنشاء الاشتراك الجديد
			var subscription = new Subscribe
			{
				User_ID = dto.User_ID,
				Coach_ID = dto.Coach_ID,
				SubscriptionType = dto.SubscriptionType,
				PaymentProof = proofPath,
				Notes = dto.Notes,
				Status ="pending",
				IsPaid = false,
				IsApproved = false,
				StartDate = DateTime.UtcNow,
				EndDate = CalculateEndDate(dto.SubscriptionType)
			};
			
			// ميثود صغيرة نحسب بيها تاريخ الانتهاء حسب النوع
			DateTime CalculateEndDate(string subscriptionType)
			{
				return subscriptionType switch
				{
					"3_Months" => DateTime.UtcNow.AddMonths(3),
					"6_Months" => DateTime.UtcNow.AddMonths(6),
					"1_Year" => DateTime.UtcNow.AddMonths(12),
					_ => throw new ArgumentException("Subscription type not supported", nameof(subscriptionType))
				};
			}
			await _repository.AddSubscriptionAsync(subscription);
			return subscription;
		}


		public async Task<List<Subscribe>> GetPendingSubscriptionsAsync()
		{
			return await _repository.GetPendingSubscriptionsAsync();
		}

		public async Task<List<Subscribe>> GetRejectedSubscriptionsAsync()
		{
			return await _repository.GetRejectedSubscriptionsAsync();
		}


		public async Task<bool> ApproveSubscriptionAsync(int subscribeId)
		{
			var subscribe = await _repository.GetSubscribeById(subscribeId);
			if (subscribe == null)
			{
				Console.WriteLine($"Subscription with id {subscribeId} not found.");
				return false;
			}

			if (subscribe.IsApproved)
			{
				Console.WriteLine($"Subscription with id {subscribeId} is already approved.");
				return false;
			}


			subscribe.IsApproved = true;
			subscribe.IsPaid = true;
			subscribe.Status = "Active";
			subscribe.User.ApplicationUser.UserType = UserType.Trainee;
			await _repository.ApproveSubscriptionAsync(subscribe);

			Console.WriteLine($"Subscription {subscribeId} approved successfully.");
			return true;
		}

		public async Task<bool> RejectSubscriptionAsync(int subscribeId)
		{
			var subscribe = await _repository.GetSubscribeById(subscribeId);
			if (subscribe == null)
			{
				return false;
			}
			subscribe.Status = "Rejected";
			subscribe.IsApproved = false;
			subscribe.IsPaid = false;
			await _repository.RejectSubscriptionAsync(subscribe);
			 return true;
		}


		public async Task<List<UserSubscriptionDto>> GetUsersSubscribedToCoachAsync(string coachId)
		{
			return await _repository.GetUsersSubscribedToCoachAsync(coachId);
		}

		public async Task<List<CoachSubscriptionDto>> GetUserSubscriptionsAsync(string userId)
		{
			return await _repository.GetUserSubscriptionsAsync(userId);
		}

	}
}
