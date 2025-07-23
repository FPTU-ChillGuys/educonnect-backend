namespace EduConnect.Application.DTOs.Responses.NotificationResponses
{
	public class NotificationDto
	{
		public Guid NotificationId { get; set; }
		public DateTime SentAt { get; set; }
		public bool IsRead { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public Guid? ClassReportId { get; set; }
		public Guid? StudentReportId { get; set; }
	}
}
