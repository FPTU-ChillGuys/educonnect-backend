namespace EduConnect.Application.DTOs.Requests.ClassSessionRequests
{
	public class UpdateClassSessionRequest
	{
		public string LessonContent { get; set; } = string.Empty;
		public int TotalAbsentStudents { get; set; }
		public string? GeneralBehaviorNote { get; set; }
	}
}
