namespace EduConnect.Application.DTOs.Requests.BehaviorRequests
{
	public class UpdateStudentBehaviorNoteRequest
	{
		public string BehaviorType { get; set; } = string.Empty;
		public string? Comment { get; set; }
	}
}
