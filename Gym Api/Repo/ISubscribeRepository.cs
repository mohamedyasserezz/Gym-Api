using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Repo
{
	public interface ISubscribeRepository
	{
		public Task<List<Subscribe>> GetAllSubscriptionsAsync();
		public Task<Subscribe> GetSubscribeById(int id);
		public Task<List<Subscribe>> GetSubscribeByUserIdAsyncR(string userId);
		public Task<bool> HasActiveSubscriptionAsync(string userId, string coachId);
		public Task<Subscribe> AddSubscriptionAsync(Subscribe subscribe);
		public Task<List<Subscribe>> GetPendingSubscriptionsAsync();
		public Task<List<Subscribe>> GetRejectedSubscriptionsAsync();
		public Task<bool> ApproveSubscriptionAsync(Subscribe subscribe);
		public Task<bool> RejectSubscriptionAsync(Subscribe subscribe);
		public Task<List<UserSubscriptionDto>> GetUsersSubscribedToCoachAsync(string coachId);
		public Task<List<CoachSubscriptionDto>> GetUserSubscriptionsAsync(string userId);


	}
}
