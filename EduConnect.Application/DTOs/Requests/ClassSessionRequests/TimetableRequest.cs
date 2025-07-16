namespace EduConnect.Application.DTOs.Requests.ClassSessionRequests
{
	public class TimetableRequest
	{
		public Guid TargetId { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public string Mode { get; set; } = string.Empty; // "Class" or "Teacher"
	}
}
