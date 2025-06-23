using EduConnect.Application.DTOs.Requests.AuthRequests;
using EduConnect.Application.DTOs.Responses.AuthResponses;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using EduConnect.Application.Commons;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController(IAuthService _authService) : ControllerBase
	{
		[HttpPost("register-for-admin")]
		[AllowAnonymous]
		public async Task<ActionResult<BaseResponse<string>>> Register([FromBody] Register request, [FromQuery] string role)
		{
			var result = await _authService.RegisterAsync(request, role);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpGet("verify-email")]
		[AllowAnonymous]
		public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string token)
		{
			var result = await _authService.VerifyEmailAsync(email, token);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<BaseResponse<TokenResponse>>> Login([FromBody] Login request)
		{
			var result = await _authService.LoginAsync(request);
			return result.Success ? Ok(result) : Unauthorized(result);
		}

		[HttpPost("refresh-token")]
		[Authorize]
		public async Task<ActionResult<BaseResponse<TokenResponse>>> RefreshToken([FromBody] RefreshTokenRequest request)
		{
			var result = await _authService.RefreshTokenAsync(request);
			return result.Success ? Ok(result) : Unauthorized(result);
		}

		[HttpPost("forgotpassword")]
		[AllowAnonymous]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(BaseResponse<string>.Fail("Invalid request"));

			var result = await _authService.ForgotPasswordAsync(request);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpPost("resetpassword")]
		[AllowAnonymous]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(BaseResponse<string>.Fail("Invalid request"));

			var result = await _authService.ResetPasswordAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
