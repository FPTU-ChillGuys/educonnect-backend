namespace EduConnect.Application.DTOs.Requests.AuthRequests
{
	public class GoogleLoginRequest
	{
		public string IdToken { get; set; } = default!;
		public string? DeviceToken { get; set; }
	}
}
