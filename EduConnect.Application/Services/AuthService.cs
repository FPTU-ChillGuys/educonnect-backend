using EduConnect.Application.DTOs.Responses.AuthResponses;
using EduConnect.Application.DTOs.Requests.AuthRequests;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.Commons.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity;
using EduConnect.Domain.Entities;
using EduConnect.Domain.Enums;
using Google.Apis.Auth;

namespace EduConnect.Application.Services
{
	public class AuthService(
								UserManager<User> _userManager,
								IAuthRepository _authRepository,
								IEmailService _emailService,
								IEmailTemplateProvider _templateProvider,
								IConfiguration config
							) : IAuthService
	{
		public async Task<BaseResponse<TokenResponse>> LoginAsync(Login login)
		{
			var user = await _userManager.FindByEmailAsync(login.Email!);
			if (user == null)
				return BaseResponse<TokenResponse>.Fail("User not found");

			if (!user.IsActive)
				return BaseResponse<TokenResponse>.Fail("User is inactive");

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, login.Password!);
			if (!isPasswordValid)
				return BaseResponse<TokenResponse>.Fail("Invalid password");

			var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
			if (!isEmailConfirmed)
				return BaseResponse<TokenResponse>.Fail("Email not confirmed");

			if (!string.IsNullOrWhiteSpace(login.DeviceToken) && user.DeviceToken != login.DeviceToken)
			{
				user.DeviceToken = login.DeviceToken;
				await _userManager.UpdateAsync(user);
			}

			var tokenResponse = await GenerateTokenResponseAsync(user);
			return BaseResponse<TokenResponse>.Ok(tokenResponse);
		}


		public async Task<BaseResponse<TokenResponse>> LoginWithGoogleAsync(GoogleLoginRequest request)
		{
			GoogleJsonWebSignature.Payload payload;
			try
			{
				payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
			}
			catch
			{
				return BaseResponse<TokenResponse>.Fail("Invalid Google token");
			}

			var email = payload.Email;
			var user = await _userManager.FindByEmailAsync(email);

			// Allow login ONLY if user exists
			if (user is null)
				return BaseResponse<TokenResponse>.Fail("Your account is not registered.");

			if (!user.IsActive)
				return BaseResponse<TokenResponse>.Fail("Your account is inactive.");

			if (!user.EmailConfirmed)
			{
				user.EmailConfirmed = true;
				await _userManager.UpdateAsync(user);
			}

			if (!string.IsNullOrWhiteSpace(request.DeviceToken) && user.DeviceToken != request.DeviceToken)
			{
				user.DeviceToken = request.DeviceToken;
			}
			await _userManager.UpdateAsync(user);

			// Generate your own JWT + refresh token
			var tokenResponse = await GenerateTokenResponseAsync(user);
			return BaseResponse<TokenResponse>.Ok(tokenResponse, "Google login successful");
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
			var verifyUrl = QueryHelpers.AddQueryString(
				register.ClientUri!,
				new Dictionary<string, string?>
				{
					{ "email", register.Email! },
					{ "token", token }
				}
			);

			var emailContent = _templateProvider.GetRegisterTemplate(
				register.Username ?? Roles.Parent.ToString(),
				verifyUrl
			);

			await _emailService.SendEmailAsync(register.Email!, "Email Confirmation", emailContent);

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

		public async Task<BaseResponse<string>> ForgotPasswordAsync(ForgotPasswordRequest request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email!);
			if (user is null)
				return BaseResponse<string>.Fail("User not found");

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var param = new Dictionary<string, string?>
			{
				{"token", token },
				{"email", request.Email!}
			};

			var callback = QueryHelpers.AddQueryString(request.ClientUri!, param);
			var emailContent = _templateProvider.GetForgotPasswordTemplate(user.UserName ?? "User", callback);

			await _emailService.SendEmailAsync(user.Email!, "Reset Password", emailContent);
			return BaseResponse<string>.Ok("Reset password email sent successfully");
		}

		public async Task<BaseResponse<string>> ResetPasswordAsync(ResetPasswordRequest request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email!);
			if (user is null)
				return BaseResponse<string>.Fail("User not found");

			var resetResult = await _userManager.ResetPasswordAsync(user, request.Token!, request.Password!);
			if (!resetResult.Succeeded)
				return BaseResponse<string>.Fail("Failed to reset password", resetResult.Errors.Select(e => e.Description).ToList());

			return BaseResponse<string>.Ok("Password reset successfully");
		}

		private async Task<bool> CheckEmailExists(string email)
		{
			var existingUser = await _userManager.FindByEmailAsync(email);
			return existingUser != null;
		}
	}
}
