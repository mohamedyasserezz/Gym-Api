using Gym_Api.Data.Models;

namespace Gym_Api.Repo
{
	public interface ISubscribeRepository
	{
		public Task<List<Subscribe>> GetAllSubscriptionsAsync();
		public Task<Subscribe> GetSubscribeById(int id);
		public Task<bool> HasActiveSubscriptionAsync(int userId, int coachId);
		public Task<Subscribe> AddSubscriptionAsync(Subscribe subscribe);
		public Task<List<Subscribe>> GetPendingSubscriptionsAsync();
		public Task<bool> ApproveSubscriptionAsync(Subscribe subscribe);
		public Task<bool> RejectSubscriptionAsync(Subscribe subscribe);


	}
}
