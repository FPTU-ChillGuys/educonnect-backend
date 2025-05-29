using EduConnect.Application.DTOs.Users;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController(IAuthService _authService, IConfiguration configuration) : ControllerBase
	{
		public static User user = new();

		//[HttpPost("register")]
		//public IActionResult Register([FromBody] Register request)
		//{
		//	var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
			
		//	user.Email = request.Email;
		//	user.PasswordHash = hashedPassword;

		//	return Ok(user);
		//}

		[HttpPost("login")] 
		public async Task<IActionResult> Login([FromBody] Login request)
		{
			var tokenResponse = await _authService.LoginAsync(request);
			if (tokenResponse == null)
			{
				return Unauthorized("Invalid email or password.");
			}
			return Ok(tokenResponse);
		}
	}
}
