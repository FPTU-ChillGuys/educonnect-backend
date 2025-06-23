using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.AuthRequests
{
	public record Login
	(
		[Required, EmailAddress]
		string Email,
		[Required]
		string Password
	);
}
