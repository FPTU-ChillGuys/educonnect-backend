namespace EduConnect.Application.DTOs.Requests.BehaviorRequests
{
	public class UpdateClassBehaviorLogRequest
	{
		public string BehaviorType { get; set; } = string.Empty;
		public string? Note { get; set; }
	}
}
