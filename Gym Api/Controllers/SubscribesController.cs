using Gym_Api.DTO;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscribesController : ControllerBase
	{
		private readonly ISubscribeService _subscribeService;

		public SubscribesController(ISubscribeService subscribeService)
		{
			_subscribeService = subscribeService;
		}




		[HttpGet("GetAllSubscribtions")]
		public async Task<IActionResult> GetAllSubscribtions()
		{
			var subscribtions = await _subscribeService.GetAllSubscriptionsAsyncS();
			return Ok(subscribtions);
		}



		[HttpGet("GetSubscribeById")]
		public async Task<IActionResult> GetSubscribeById(int id)
		{
			var subscribe = await _subscribeService.GetSubscribeByIdS(id);
			if(subscribe == null)
			{
				return NotFound($"Subscribe with id {id} not found");
			}
			return Ok(subscribe);
		}




		[HttpPost("AddNewSubscribe")]
		public async Task<IActionResult> CreateSubscription([FromForm] CreateSubscriptionDto dto)
		{
			var result = await _subscribeService.CreateSubscriptionAsync(dto);
			return Ok(result);
		}





		// ✅ Get all pending subscriptions
		[HttpGet("pending")]
		public async Task<IActionResult> GetPendingSubscriptions()
		{
			var pendingSubscriptions = await _subscribeService.GetPendingSubscriptionsAsync();
			return Ok(pendingSubscriptions);
		}




		[HttpPut("approve/{id}")]
		public async Task<IActionResult> ApproveSubscription(int id)
		{
			var result = await _subscribeService.ApproveSubscriptionAsync(id);
			if (!result)
				return BadRequest("فشل في الموافقة على الاشتراك. تحقق من حالة الاشتراك أو الدفع.");

			return Ok("تمت الموافقة على الاشتراك بنجاح.");
		}




		[HttpPut("reject/{id}")]
		public async Task<IActionResult> RejectSubscription(int id)
		{
			var result = await _subscribeService.RejectSubscriptionAsync(id);
			if (!result)
				return BadRequest("فشل في رفض الاشتراك. تحقق من حالة الاشتراك أو الدفع.");

			return Ok("تم رفض الاشتراك وحذفه بنجاح.");
		}


		// جلب الطلاب المشتركين مع كوتش
		[HttpGet("coach/{coachId}")]
		public async Task<IActionResult> GetUsersSubscribedToCoach(int coachId)
		{
			var result = await _subscribeService.GetUsersSubscribedToCoachAsync(coachId);
			return Ok(result);
		}



		// جلب الاشتراكات الخاصة باليوزر
		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetUserSubscriptions(int userId)
		{
			var result = await _subscribeService.GetUserSubscriptionsAsync(userId);
			return Ok(result);
		}


	}
}
