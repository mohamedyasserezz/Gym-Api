using Gym_Api.Common;
using Gym_Api.DTO.Authentication.ConfirmEmail;
using Gym_Api.DTO.Authentication.ForgetPassword;
using Gym_Api.DTO.Authentication.Login;
using Gym_Api.DTO.Authentication.RefreshToken;
using Gym_Api.DTO.Authentication.Register;
using Gym_Api.DTO.Authentication.ResendConfirmationEmail;
using Gym_Api.DTO.ResetPassword;
using Gym_Api.Survices.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gym_Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthController(IAuthServices authServices,
	ILogger<AuthController> logger,
	IOptions<JwtOptions> options
	) : ControllerBase
	{
		private readonly IAuthServices _authServices = authServices;
		private readonly ILogger<AuthController> _logger = logger;
		private readonly JwtOptions options = options.Value;

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Register new user with email: {email} and password: {password}", registerRequest.Email, registerRequest.Password);
			var response = await _authServices.RegisterAsync(registerRequest, cancellationToken);
			return response.IsSuccess ? Ok() : response.ToProblem();

		}

		[HttpPost("Confirm-email")]
		public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("try confirm user with user with id: {id}", request.UserId);

			var response = await _authServices.ConfirmEmailAsync(request);

			return response.IsSuccess ? Ok() : response.ToProblem();

		}

		[HttpPost("resend-Confirm-email")]
		public async Task<IActionResult> ResendConfirmEmail([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("try to resend email for the user with Email: {Email}", request.Email);

			var response = await _authServices.ResendConfirmationEmail(request);

			return response.IsSuccess ? Ok() : response.ToProblem();

		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Logging with email: {email} and password: {password}", loginRequest.Email, loginRequest.Password);

			var response = await _authServices.GetTokenAsync(loginRequest.Email, loginRequest.Password, cancellationToken);

			return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
		}
		[HttpPost("refresh")]
		public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
		{
			var response = await _authServices.GetRefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken, cancellationToken);

			return response.IsSuccess ? Ok(response.Value) : response.ToProblem();

			//return response.Value is null ? Problem(statusCode: StatusCodes.Status404NotFound,
			//                                            title: response.Error.code,
			//                                            detail: response.Error.Description)
			//                                         : Ok(response.Value);
		}

		[HttpPost("Revoke-refresh-Token")]
		public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
		{
			var response = await _authServices.RevokeRefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken, cancellationToken);

			return response.IsSuccess ? Ok() : response.ToProblem();

			// return response != Result.Success() ? Problem(statusCode: StatusCodes.Status404NotFound, title: response.Error.code, detail: response.Error.Description) : Ok();
		}

		[HttpPost("forget-password")]
		public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest forgetPasswordRequest)
		{
			var response = await _authServices.SendResetPasswordCodeAsync(forgetPasswordRequest.Email);
			return response.IsSuccess ? Ok() : response.ToProblem();
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetPasswordRequest)
		{
			var response = await _authServices.ResetPasswordAsync(resetPasswordRequest);
			return response.IsSuccess ? Ok() : response.ToProblem();
		}
	}
}
