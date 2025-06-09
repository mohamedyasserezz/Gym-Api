using Gym_Api.Data.Models;
using Gym_Api.DTO;

namespace Gym_Api.Survices
{
	public interface ISubscribeService
	{
		public Task<List<Subscribe>> GetAllSubscriptionsAsyncS();
        public Task<Subscribe> GetSubscribeByIdS(int id);
		public Task<List<Subscribe>> GetSubscribeByUseridAsync(string userId);
		public Task<Subscribe> CreateSubscriptionAsync(CreateSubscriptionDto dto);
		public Task<List<Subscribe>> GetPendingSubscriptionsAsync();
		public Task<List<Subscribe>> GetRejectedSubscriptionsAsync();
		public Task<bool> ApproveSubscriptionAsync(int subscribeId);
		public Task<bool> RejectSubscriptionAsync(int subscribeId);
		public Task<List<UserSubscriptionDto>> GetUsersSubscribedToCoachAsync(string coachId);
		public Task<List<CoachSubscriptionDto>> GetUserSubscriptionsAsync(string userId);


	}
}
