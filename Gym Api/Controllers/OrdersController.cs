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


	}
}
