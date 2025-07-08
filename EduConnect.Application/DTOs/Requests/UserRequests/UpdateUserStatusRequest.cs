namespace EduConnect.Application.DTOs.Requests.UserRequests
{
	public class UpdateUserStatusRequest
	{
		public bool IsActive { get; set; } = false; // Default to false for deactivation
	}
}
