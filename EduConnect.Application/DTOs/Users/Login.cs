using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Users
{
	public record Login
	(
		[Required, EmailAddress]
		string Email,
		[Required]
		string Password
	);
}
