namespace EduConnect.Application.DTOs.Responses.ClassSessionResponses
{
	public class TimetableViewDto
	{
		public DateTime Date { get; set; }
		public List<PeriodSlotDto> Periods { get; set; } = new();
	}

	public class PeriodSlotDto
	{
		public int PeriodNumber { get; set; }
		public string? ClassName { get; set; }
		public string? SubjectName { get; set; }
		public string? TeacherName { get; set; }
		public string? LessonContent { get; set; }
	}
}
