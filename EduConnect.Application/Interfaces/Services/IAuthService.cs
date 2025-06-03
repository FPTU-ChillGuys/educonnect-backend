using EduConnect.Application.Commons;
using EduConnect.Application.DTOs.Requests;
using EduConnect.Application.DTOs.Responses;
using EduConnect.Application.DTOs.Users;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IAuthService
	{
		public Task<BaseResponse<string>> RegisterAsync(Register register, string role);
		public Task<BaseResponse<string>> VerifyEmailAsync(string email, string token);
		public Task<BaseResponse<TokenResponse>> LoginAsync(Login login);
		public Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
	}
}
