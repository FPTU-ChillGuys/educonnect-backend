using EduConnect.Domain.Entities;
namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IAuthRepository
	{
		public string GenerateJwtToken(User user, string role);
		public Task<string> GenerateAndSaveRefreshToken(User user);
		public Task<User> ValidateRefreshToken(Guid userId, string refreshToken);
	}
}
