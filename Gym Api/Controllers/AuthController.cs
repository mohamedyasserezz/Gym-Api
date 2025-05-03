using Gym_Api.Common;
using Gym_Api.Data.Models;
using Gym_Api.DTO.Authentication.CompleteProfile;
using Gym_Api.DTO.Authentication.ConfirmEmail;
using Gym_Api.DTO.Authentication.ForgetPassword;
using Gym_Api.DTO.Authentication.Login;
using Gym_Api.DTO.Authentication.RefreshToken;
using Gym_Api.DTO.Authentication.Register;
using Gym_Api.DTO.Authentication.ResendConfirmationEmail;
using Gym_Api.DTO.Authentication.ResetPassword;
using Gym_Api.Survices.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Gym_Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
    public class AuthController(
        IAuthServices authService,
        ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthServices _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Logging with email: {email} and password: {password}", loginRequest.Email, loginRequest.Password);

            var response = await _authService.GetTokenAsync(loginRequest.Email, loginRequest.Password, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
        {
            var response = await _authService.GetRefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : response.ToProblem();

        }

        [HttpPost("Revoke-refresh-Token")]
        public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
        {
            var response = await _authService.RevokeRefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken, cancellationToken);

            return response.IsSuccess ? Ok() : response.ToProblem();

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmEmailAsync(request);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPost("resend-Confirm-email")]
        public async Task<IActionResult> ResendConfirmEmail([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("try to resend email for the user with Email: {Email}", request.Email);

            var response = await _authService.ResendConfirmationEmailAsync(request);

            return response.IsSuccess ? Ok() : response.ToProblem();

        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            var result = await _authService.SendResetPasswordOtpAsync(request.Email);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpPost("register-otp-for-new-password")]
        public async Task<IActionResult> AssignOtpForPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.AssignOtpForPassword(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] AssignNewPassword request)
        {
            var result = await _authService.ResetPasswordAsync(request);

            return result.IsSuccess ? Ok() : result.ToProblem();
        }


        [HttpPost("AddNewCoach")]
        public async Task<IActionResult> AddNewCoach([FromBody] RegisterCoachRequest registerCoach)
        {
           var result= await _authService.CompleteCoachRegistration(registerCoach);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }






        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody]RegsisterUserRequest userRequest)
        {
            var result = await _authService.CompleteUserRegistration(userRequest);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();

        }


    }
}
