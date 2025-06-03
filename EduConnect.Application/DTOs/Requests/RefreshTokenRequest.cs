namespace EduConnect.Application.DTOs.Requests
{
	public class RefreshTokenRequest
	{
		public required Guid UserId { get; set; }
		public required string RefreshToken { get; set; }
	}
}
