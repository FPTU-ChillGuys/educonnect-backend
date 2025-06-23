using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.AuthRequests
{
	public class ForgotPasswordRequest
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		public string? ClientUri { get; set; } //https://localhost:7299/api/auth/resetpassword
	}
}
