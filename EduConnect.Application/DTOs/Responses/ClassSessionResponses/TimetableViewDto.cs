namespace EduConnect.Application.DTOs.Responses.ClassSessionResponses
{
	public class TimetableViewDto
	{
		public DateTime Date { get; set; }
		public string DayOfWeek { get; set; } = string.Empty;
		public List<PeriodSlotDto> Periods { get; set; } = new();
	}

	public class PeriodSlotDto
	{
		public Guid ClassSessionId { get; set; }
		public int PeriodNumber { get; set; }
		public Guid ClassId { get; set; }
		public string? ClassName { get; set; }
		public Guid SubjectId { get; set; }
		public string? SubjectName { get; set; }
		public Guid TeacherId { get; set; }
		public string? TeacherName { get; set; }
		public string? LessonContent { get; set; }
		public string? GeneralBehaviorNote { get; set; }
		public int TotalAbsentStudents { get; set; }

	}
}
