using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.AuthRequests
{
	public class Register
	{
		[Required(ErrorMessage = "Please input username!")]
		[RegularExpression(@"^[\p{L}0-9_]{3,20}$", ErrorMessage = "Username can only contain letters (including Vietnamese), numbers, and underscores (3-20 chars).")]
		public string? Username { get; set; }

		[Required(ErrorMessage = "Please input email!")]
		[EmailAddress(ErrorMessage = "Invalid email address!")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please input password!")]
		public string Password { get; set; } = string.Empty;

		[Required]
		public string? ClientUri { get; set; } //https://localhost:7299/api/auth/verify-email
	}
}
