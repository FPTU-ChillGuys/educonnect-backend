using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.AuthRequests
{
	public class Login
	{
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? DeviceToken { get; set; } 
	}
}
