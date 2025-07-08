namespace EduConnect.Application.DTOs.Requests.ClassSessionRequests
{
	public class UpdateClassSessionByAdminRequest
	{
		public Guid ClassId { get; set; }
		public Guid SubjectId { get; set; }
		public Guid TeacherId { get; set; }
		public DateTime Date { get; set; }
		public int PeriodNumber { get; set; }
		public string LessonContent { get; set; } = string.Empty;
		public int TotalAbsentStudents { get; set; }
		public string? GeneralBehaviorNote { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
