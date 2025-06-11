using Gym_Api.DTO;
using Gym_Api.Services;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;
		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}


		[HttpGet("GetAllOrders")]
		public async Task<IActionResult> GetAllOrders()
		{
			var orders = await _orderService.GetAllOrdersAsync();
			return Ok(orders);
		}





		[HttpGet("GetOrdersByUserId/{Userid}")]
		public async Task<IActionResult> GetByUserId(string Userid)
		{
			var order = await _orderService.GetAllUserOrdersAsync(Userid);
			if (order == null || order.Count == 0)
			{
				return NotFound($"No Orders found for user ID: {Userid}");

			}
			return Ok(order);
		}


		[HttpGet("GetOrderById/{id}")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			var order = await _orderService.GetOrderByIdAsync(id);
			if (order == null)
			{
				return NotFound($"Order with id {id} not found");
			}
			return Ok(order);
		}


		[HttpPost("AddNewOrder")]
		public async Task<IActionResult> AddNewOrder([FromForm] CreateOrderDto createOrderDto)
		{
			var result = await _orderService.AddOrderAsync(createOrderDto);
			return Ok(result);
		}



		[HttpGet("GetAllpendingOrders")]
		public async Task<IActionResult> GetPendingOrders()
		{
			var pendingorders = await _orderService.GetAllPendingOrdersAsync();
			return Ok(pendingorders);
		}



		[HttpPut("ApproveOrder/{id}")]
		public async Task<IActionResult> ApproveOrder(int id)
		{

			var result = await _orderService.AcceptOrderAsync(id);
			if (!result)
			{
				return BadRequest("فشل في الموافقة على الطلب.");
			}

			return Ok("تمت الموافقة على الطلب بنجاح.");
		}




		[HttpPut("RejectOrder/{id}")]
		public async Task<IActionResult> RejectOrder(int id)
		{
			var result = await _orderService.RejectOrderAsync(id);
			if (!result)
				return BadRequest("فشل في رفض الطلب. تحقق من حالة الطلب أو الدفع.");

			return Ok("تم رفض الطلب بنجاح");
		}

		[HttpPut("UpdateOrder/{OrderId}")]
		public async Task<IActionResult> Updateorder(int OrderId, [FromBody] UpdateOrderDto dto)
		{
			var result = await _orderService.UpdateOrderAsync(OrderId, dto);
			if (!result)
			{
				return NotFound("Order not found");
			}
			return Ok("Order Updated Successfuly");
		}

	}
}
