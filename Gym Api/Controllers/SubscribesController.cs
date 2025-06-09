using Gym_Api.Common.Consts;
using Gym_Api.Data.Models;
using Gym_Api.DTO;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Authorization;
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



		[HttpGet("GetSubscribeById/{id}")]
		public async Task<IActionResult> GetSubscribeById(int id)
		{
			var subscribe = await _subscribeService.GetSubscribeByIdS(id);
			if(subscribe == null)
			{
				return NotFound($"Subscribe with id {id} not found");
			}
			return Ok(subscribe);
		}


		[HttpGet("GetSubscribeByUserId/{Userid}")]
		public async Task<IActionResult> GetByUserId(string Userid)
		{
			var subsccibe = await _subscribeService.GetSubscribeByUseridAsync(Userid);
			if (subsccibe == null || subsccibe.Count == 0)
			{
				return NotFound($"No subscriptions found for user ID: {Userid}");
				
			}
			return Ok(subsccibe);
		}


		[HttpPost("AddNewSubscribe")]
		public async Task<IActionResult> CreateSubscription([FromForm] CreateSubscriptionDto dto)
		{
			var subscription = await _subscribeService.CreateSubscriptionAsync(dto);

			if (subscription == null)
				return BadRequest("ربما أنت مشترك بالفعل مع هذا الكوتش، أو حدث خطأ أثناء إنشاء الاشتراك. يُرجى التحقق والمحاولة مرة أخرى.");

			return Ok(subscription); 
		}





		// ✅ Get all pending subscriptions
		[HttpGet("GetAllpendingsubscriptions")]
		public async Task<IActionResult> GetPendingSubscriptions()
		{
			var pendingSubscriptions = await _subscribeService.GetPendingSubscriptionsAsync();
			return Ok(pendingSubscriptions);
		}


		[HttpGet("GetAllrejectedsubscriptions")]
		public async Task<IActionResult> GetRejectedSubscriptions()
		{
			var subscriptions = await _subscribeService.GetRejectedSubscriptionsAsync();
			return Ok(subscriptions);
		}




		[HttpPut("approve/{id}")]
		public async Task<IActionResult> ApproveSubscription(int id)
		{

			var result = await _subscribeService.ApproveSubscriptionAsync(id);
			if (!result)
			{
				return BadRequest("فشل في الموافقة على الاشتراك.");
			}

			return Ok("تمت الموافقة على الاشتراك بنجاح.");
		}




		[HttpPut("reject/{id}")]
		public async Task<IActionResult> RejectSubscription(int id)
		{
			var result = await _subscribeService.RejectSubscriptionAsync(id);
			if (!result)
				return BadRequest("فشل في رفض الاشتراك. تحقق من حالة الاشتراك أو الدفع.");

			return Ok("تم رفض الاشتراك بنجاح");
		}


		// جلب الطلاب المشتركين مع كوتش
		[HttpGet("coach/{coachId}")]
		public async Task<IActionResult> GetUsersSubscribedToCoach(string coachId)
		{
			var result = await _subscribeService.GetUsersSubscribedToCoachAsync(coachId);
			return Ok(result);
		}



		// جلب الاشتراكات الخاصة باليوزر
		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetUserSubscriptions(string userId)
		{
			var result = await _subscribeService.GetUserSubscriptionsAsync(userId);
			return Ok(result);
		}


	}
}
