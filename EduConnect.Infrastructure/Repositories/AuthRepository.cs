using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Domain.Entities;
using EduConnect.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EduConnect.Infrastructure.Repositories
{
	public class AuthRepository(EduConnectDbContext _context, IConfiguration configuration) : IAuthRepository
	{
		public string GenerateJwtToken(User user, string role)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.Role, role)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Key"] ?? throw new Exception("Missing JWT Key")));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
			var token = new JwtSecurityToken(
				issuer: configuration["Authentication:Issuer"] ?? throw new Exception("Missing Issuer"),
				audience: configuration["Authentication:Audience"] ?? throw new Exception("Missing Audience"),
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<string> GenerateAndSaveRefreshToken(User user)
		{
			var refreshToken = GenerateRefreshToken();
			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
			await _context.SaveChangesAsync();
			return refreshToken;
		}

		public async Task<User> ValidateRefreshToken(Guid userId, string refreshToken)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
			{
				return null!;
			}
			return user;
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
			}
			return Convert.ToBase64String(randomNumber);
		}
	}
}
