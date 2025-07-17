using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.AuthRequests
{
	public class RegisterRequest
	{
		public string? Username { get; set; }

		public string? FullName { get; set; }

		public string Email { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public string? ClientUri { get; set; } //https://localhost:7299/api/auth/verify-email
	}
}
