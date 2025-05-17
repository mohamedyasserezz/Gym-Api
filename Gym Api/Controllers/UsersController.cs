using Gym_Api.DTO;
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



		[HttpGet("Getuserbyid/{id}")]
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




		[HttpPut("UpdateUser/{userId}")]
		public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto dto)
		{
			var success = await _userService.UpdateUserdataAsync(userId, dto);
			if (!success)
			{
				return NotFound("User not found");
			}
			return Ok("User Updated Successfuly");
		}




		[HttpDelete("DeleteUser/{userId}")]
		public async Task<IActionResult> DeleteUser(string userId)
		{
			var success = await _userService.DeleteUserdataAsync(userId);
			if (!success)
			{
				return NotFound("User not found");
			}
			return Ok("User Deleted Successfuly");
		}




	}
}
