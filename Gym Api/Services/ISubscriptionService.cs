using Gym_Api.Contract;
using Gym_Api.Data.Models;

namespace Gym_Api.Survices
{
	public interface ISubscriptionService
	{
		public Task<string> AddSubscriptionAsync(CreateSubscriptionDto createSubscriptionDto);
		public Task<string> ConfirmPaymentAsync(int SubscriptioonId);
		public Task<List<Subscribe>> GetAllPaidSubscriptionsAsync();
		public Task<string> ApproveSubscriptionPaymentAsync(int subscriptionid);
		public Task<string> RejectSubscriptionAsync (int subscriptionid);
		public Task<List<Coach>> GetUserSubscriptionsAsync(int userid);
		public Task<List<User>> GetCoachSubscribersAsync(int coachid);
		public Task<bool> CancelSubscriptionAsync(int subscriptionId, int userId);


	}
}
