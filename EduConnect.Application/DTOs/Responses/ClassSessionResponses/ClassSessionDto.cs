namespace EduConnect.Application.DTOs.Responses.ClassSessionResponses
{
	public class ClassSessionDto
	{
		public Guid ClassSessionId { get; set; }
		public Guid ClassId { get; set; }
		public string ClassName { get; set; }
		public Guid SubjectId { get; set; }
		public string SubjectName { get; set; }
		public Guid TeacherId { get; set; }
		public string TeacherName { get; set; }
		public DateTime Date { get; set; }
		public int PeriodNumber { get; set; }
		public string LessonContent { get; set; }
		public int TotalAbsentStudents { get; set; }
		public string? GeneralBehaviorNote { get; set; }
	}
}
