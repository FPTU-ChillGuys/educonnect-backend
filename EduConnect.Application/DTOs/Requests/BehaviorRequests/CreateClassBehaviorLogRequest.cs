namespace EduConnect.Application.DTOs.Requests.BehaviorRequests
{
	public class CreateClassBehaviorLogRequest
	{
		public Guid ClassSessionId { get; set; }
		public string BehaviorType { get; set; } = string.Empty;
		public string? Note { get; set; }
	}
}
