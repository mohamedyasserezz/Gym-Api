using Gym_Api.Contract;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscriptionsController : ControllerBase
	{
		private readonly ISubscriptionService _subscriptionService;
		public SubscriptionsController(ISubscriptionService subscriptionService)
		{
			_subscriptionService = subscriptionService;
		}

		[HttpPost]
		public async Task<IActionResult> AddSubscription([FromBody] CreateSubscriptionDto createSubscriptionDto)
		{
			var newsubscribe = await _subscriptionService.AddSubscriptionAsync(createSubscriptionDto);
			if (newsubscribe == null) 
			{
				return NotFound("user already subscribe with this coach");
			}
			return Ok(new{ message = "Subscription added successfully", newsubscribe });
		}



		[HttpPost("User confirm-payment/{subscriptionId}")]
		public async Task<IActionResult> ConfirmPayment(int subscriptionId)
		{
			var result = await _subscriptionService.ConfirmPaymentAsync(subscriptionId);
			return Ok(result);
		}



		[HttpGet("admin/paid-subscriptions")]
		public async Task<IActionResult> GetAllPaidSubscriptions()
		{
			var subscriptions = await _subscriptionService.GetAllPaidSubscriptionsAsync();

			if (subscriptions.Count == 0)
			{
				return Ok("لا يوجد اشتراكات مدفوعة في انتظار المراجعة.");
			}

			return Ok(subscriptions);
		}


		[HttpPut("admin/approve-payment/{subscriptionId}")]
		public async Task<IActionResult> ApproveSubscriptionPayment(int subscriptionId)
		{
			var result = await _subscriptionService.ApproveSubscriptionPaymentAsync(subscriptionId);

			if (result == "هذا الاشتراك غير موجود.")
				return NotFound(result);

			if (result == "لا يمكن الموافقة على اشتراك لم يتم دفعه بعد." ||
				result == "تمت الموافقة على هذا الاشتراك بالفعل.")
				return BadRequest(result);

			return Ok(result);
		}


		[HttpPut("admin/reject-subscription/{subscriptionId}")]
		public async Task<IActionResult> RejectSubscription(int subscriptionId)
		{
			var result = await _subscriptionService.RejectSubscriptionAsync(subscriptionId);

			if (result == "الاشتراك غير موجود.")
			{
				return NotFound(result);
			}

			return Ok(result);
		}


		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetUserSubscriptions(int userId)
		{
			var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
			if (subscriptions == null)
			{
				return NotFound($"User with ID {userId} not found.");
			}
			if (!subscriptions.Any())
			{
				return NotFound($"No subscriptions found for user with ID {userId}.");
			}
			return Ok(subscriptions);
		}



		[HttpGet("CoachId/{CoachId}")]
		public async Task<IActionResult> GetCoachesSubscriptions(int CoachId)
		{
			var subscriptions = await _subscriptionService.GetCoachSubscribersAsync(CoachId);
			if (subscriptions == null)
			{
				return NotFound($"coach with ID {CoachId} not found.");
			}
			if (!subscriptions.Any())
			{
				return NotFound($"No subscriptions found for coach with ID {CoachId}.");
			}
			return Ok(subscriptions);
		}



		[HttpDelete("{subscriptionId}/{userId}")]
		public async Task<IActionResult> CancelSubscription(int subscriptionId, int userId)
		{
			var result = await _subscriptionService.CancelSubscriptionAsync(subscriptionId, userId);

			if (!result)
				return NotFound("Subscription not found or does not belong to this user.");

			return Ok("Subscription canceled successfully.");
		}




	}
}
