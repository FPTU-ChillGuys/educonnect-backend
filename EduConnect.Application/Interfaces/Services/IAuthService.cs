using EduConnect.Application.Commons;
using EduConnect.Application.DTOs.Requests.AuthRequests;
using EduConnect.Application.DTOs.Responses.AuthResponses;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IAuthService
	{
		Task<BaseResponse<string>> RegisterAsync(Register register, string role);
		Task<BaseResponse<string>> VerifyEmailAsync(string email, string token);
		Task<BaseResponse<TokenResponse>> LoginAsync(Login login);
		Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
		Task<BaseResponse<string>> ForgotPasswordAsync(ForgotPasswordRequest request);
		Task<BaseResponse<string>> ResetPasswordAsync(ResetPasswordRequest request);
	}
}
