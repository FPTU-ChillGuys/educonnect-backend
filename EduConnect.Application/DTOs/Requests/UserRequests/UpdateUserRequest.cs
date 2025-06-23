namespace EduConnect.Application.DTOs.Requests.UserRequests
{
	public class UpdateUserRequest
	{
		public string? FullName { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
