using EduConnect.Application.Commons;
using EduConnect.Application.DTOs.Requests;
using EduConnect.Application.DTOs.Responses;
using EduConnect.Application.DTOs.Users;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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

			if (!result.Success)
				return BadRequest(result);

			return Ok(result);
		}

		[HttpGet("verify-email")]
		[AllowAnonymous]
		public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string token)
		{
			var result = await _authService.VerifyEmailAsync(email, token);
			if (!result.Success)
			{
				return NotFound();
			}

			return Ok(result);
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<BaseResponse<TokenResponse>>> Login([FromBody] Login request)
		{
			var result = await _authService.LoginAsync(request);

			if (!result.Success)
				return Unauthorized(result);

			return Ok(result);
		}

		[HttpPost("refresh-token")]
		[Authorize]
		public async Task<ActionResult<BaseResponse<TokenResponse>>> RefreshToken([FromBody] RefreshTokenRequest request)
		{
			var result = await _authService.RefreshTokenAsync(request);

			if (!result.Success)
				return Unauthorized(result);

			return Ok(result);
		}
	}
}
