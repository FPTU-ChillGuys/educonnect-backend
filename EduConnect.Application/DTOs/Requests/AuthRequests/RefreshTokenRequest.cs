namespace EduConnect.Application.DTOs.Requests.AuthRequests
{
	public class RefreshTokenRequest
	{
		public required Guid UserId { get; set; }
		public required string RefreshToken { get; set; }
	}
}
