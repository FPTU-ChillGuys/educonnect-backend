using EduConnect.Application.DTOs.Requests;
using EduConnect.Application.DTOs.Responses;
using EduConnect.Application.DTOs.Users;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EduConnect.Infrastructure.Services
{
	public class AuthService(UserManager<User> _userManager, IAuthRepository _authRepository) : IAuthService
	{
		public async Task<TokenResponse> LoginAsync(Login login)
		{
			var user = await _userManager.FindByEmailAsync(login.Email!);
			if (user == null!)
			{
				return (null!);
			}

			var role = (await _userManager.GetRolesAsync(user!))[0];

			var isPasswordValid = await _userManager.CheckPasswordAsync(user!, login.Password!);
			if (!isPasswordValid)
			{
				return (null!);
			}

			var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user!);
			if (!isEmailConfirmed)
			{
				return (null!);
			}

			return await GenerateTokenResponseAsync(user);
		}

		//public async Task<User> RegisterAsync(Register register)
		//{
		//	if (await CheckEmailExists(register.Email!))
		//	{
		//		return (null!);
		//	}

		//	var user = new User
		//	{
		//		Email = register.Email,
		//		UserName = register.Username,
		//		IsActive = true
		//	};
		//	var identityResult = await _userManager.CreateAsync(user, requestBody.Password!);
		//	if (!identityResult.Succeeded)
		//	{
		//		return ("failedToCreate", null);
		//	}

		//	// Generate role when register
		//	identityResult = await _userManager.AddToRoleAsync(user, role);
		//	if (!identityResult.Succeeded)
		//	{
		//		return ("invalidRoles", null);
		//	}

		//	var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

		//	return ("success", token)
		//}

		public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
		{
			var user = await _authRepository.ValidateRefreshToken(request.UserId, request.RefreshToken);
			if (user is null)
			{
				return null!;
			}
			return await GenerateTokenResponseAsync(user);
		}

		private async Task<TokenResponse> GenerateTokenResponseAsync(User user)
		{
			var role = (await _userManager.GetRolesAsync(user!))[0];
			return new TokenResponse()
			{
				AccessToken = _authRepository.GenerateJwtToken(user!, role),
				RefreshToken = await _authRepository.GenerateAndSaveRefreshToken(user!)
			};
		}

		private async Task<bool> CheckEmailExists(string email)
		{
			var existingUser = await _userManager.FindByEmailAsync(email);
			if (existingUser == null)
			{
				return false;
			}
			return true;
		}
	}
}
