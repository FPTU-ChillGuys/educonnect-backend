namespace EduConnect.Application.DTOs.Responses.NotificationResponses
{
	public class NotificationDto
	{
		public Guid NotificationId { get; set; }
		public DateTime SentAt { get; set; }
		public bool IsRead { get; set; }

		public Guid? ClassReportId { get; set; }
		public Guid? StudentReportId { get; set; }
	}
}
