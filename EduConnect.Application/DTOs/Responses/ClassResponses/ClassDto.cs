namespace EduConnect.Application.DTOs.Responses.ClassResponses
{
	public class ClassDto
	{
		public Guid ClassId { get; set; }
		public string GradeLevel { get; set; }
		public string ClassName { get; set; }
		public string AcademicYear { get; set; }
		public string? HomeroomTeacherEmail { get; set; }
	}
}
