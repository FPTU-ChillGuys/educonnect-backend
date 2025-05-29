using EduConnect.Application.DTOs.Requests;
using EduConnect.Application.DTOs.Responses;
using EduConnect.Application.DTOs.Users;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IAuthService
	{
		//public Task<User> RegisterAsync(Register register);
		public Task<TokenResponse> LoginAsync(Login login);
		public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
	}
}
