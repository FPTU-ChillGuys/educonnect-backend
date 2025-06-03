using EduConnect.Application.Commons;
using EduConnect.Application.DTOs.Requests;
using EduConnect.Application.DTOs.Responses;
using EduConnect.Application.DTOs.Users;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EduConnect.Application.Services
{
	public class AuthService(
								UserManager<User> _userManager,
								IAuthRepository _authRepository,
								IEmailService emailService,
								IEmailTemplateProvider templateProvider,
								IConfiguration config
							) : IAuthService
	{
		public async Task<BaseResponse<TokenResponse>> LoginAsync(Login login)
		{
			var user = await _userManager.FindByEmailAsync(login.Email!);
			if (user == null)
				return BaseResponse<TokenResponse>.Fail("User not found");

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, login.Password!);
			if (!isPasswordValid)
				return BaseResponse<TokenResponse>.Fail("Invalid password");

			var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
			if (!isEmailConfirmed)
				return BaseResponse<TokenResponse>.Fail("Email not confirmed");

			var tokenResponse = await GenerateTokenResponseAsync(user);
			return BaseResponse<TokenResponse>.Ok(tokenResponse);
		}

		public async Task<BaseResponse<string>> RegisterAsync(Register register, string role)
		{
			if (await CheckEmailExists(register.Email!))
				return BaseResponse<string>.Fail("Email already exists");

			var user = new User
			{
				Email = register.Email,
				UserName = register.Username,
				IsActive = true
			};

			var identityResult = await _userManager.CreateAsync(user, register.Password!);
			if (!identityResult.Succeeded)
				return BaseResponse<string>.Fail("Failed to create user", identityResult.Errors.Select(e => e.Description).ToList());

			var roleResult = await _userManager.AddToRoleAsync(user, role);
			if (!roleResult.Succeeded)
				return BaseResponse<string>.Fail("Failed to assign role", roleResult.Errors.Select(e => e.Description).ToList());

			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			// Send verification email
			var clientUri = config["Clients:Uri"];
			var verifyUrl = $"{clientUri}/api/auth/verify-email?email={Uri.EscapeDataString(register.Email!)}&token={Uri.EscapeDataString(token!)}";
			var emailContent = templateProvider.GetRegisterTemplate(register.Username ?? Roles.Parent.ToString(), verifyUrl);
			await emailService.SendEmailAsync(register.Email!, "Email Confirmation", emailContent);

			return BaseResponse<string>.Ok(token, "User registered successfully, Please check your email to verify!");
		}

		public async Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
		{
			var user = await _authRepository.ValidateRefreshToken(request.UserId, request.RefreshToken);
			if (user is null)
				return BaseResponse<TokenResponse>.Fail("Invalid refresh token");

			var tokenResponse = await GenerateTokenResponseAsync(user);
			return BaseResponse<TokenResponse>.Ok(tokenResponse);
		}

		public async Task<BaseResponse<string>> VerifyEmailAsync(string email, string token)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return BaseResponse<string>.Fail("User not found");
			}

			var result = await _userManager.ConfirmEmailAsync(user, token);
			if (!result.Succeeded)
			{
				return BaseResponse<string>.Fail("failed to verify");
			}

			return BaseResponse<string>.Ok("Success");
		}

		private async Task<TokenResponse> GenerateTokenResponseAsync(User user)
		{
			var role = (await _userManager.GetRolesAsync(user))[0];
			return new TokenResponse()
			{
				AccessToken = _authRepository.GenerateJwtToken(user, role),
				RefreshToken = await _authRepository.GenerateAndSaveRefreshToken(user)
			};
		}

		private async Task<bool> CheckEmailExists(string email)
		{
			var existingUser = await _userManager.FindByEmailAsync(email);
			return existingUser != null;
		}
	}
}
