using Gym_Api.Services;
using Gym_Api.Survices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		public UsersController(IUserService userService)
		{
			_userService = userService;
		}



		[HttpGet("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _userService.GetAllUsersAsync();
			return Ok(users);
		}



		[HttpGet("Getuserbyid{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			var user = await _userService.GetUserByidAsync(id);
			if(user == null)
			{
				return NotFound($"user with id {id} not found");
			}
			return Ok(user);
		}



		[HttpGet("Getuserscount")]
		public async Task<IActionResult> GetUsersCount()
		{
			var users = await _userService.GetUsersCountAsync();	
			return Ok(users);
		}







	}
}
