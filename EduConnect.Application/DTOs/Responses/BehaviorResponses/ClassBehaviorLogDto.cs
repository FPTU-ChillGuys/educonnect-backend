namespace EduConnect.Application.DTOs.Responses.BehaviorResponses
{
	public class ClassBehaviorLogDto
	{
		public Guid LogId { get; set; }
		public string BehaviorType { get; set; }
		public string? Note { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
