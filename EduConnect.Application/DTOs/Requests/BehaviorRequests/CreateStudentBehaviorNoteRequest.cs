namespace EduConnect.Application.DTOs.Requests.BehaviorRequests
{
	public class CreateStudentBehaviorNoteRequest
	{
		public Guid ClassSessionId { get; set; }
		public Guid StudentId { get; set; }
		public string BehaviorType { get; set; } = string.Empty;
		public string? Comment { get; set; }
	}
}
