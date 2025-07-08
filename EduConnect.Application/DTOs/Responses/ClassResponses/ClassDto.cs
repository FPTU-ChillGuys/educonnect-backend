namespace EduConnect.Application.DTOs.Responses.ClassResponses
{
	public class ClassDto
	{
		public Guid ClassId { get; set; }
		public string GradeLevel { get; set; } = string.Empty;
		public string ClassName { get; set; } = string.Empty;
		public string AcademicYear { get; set; } = string.Empty;

		public Guid HomeroomTeacherId { get; set; }
		public string HomeroomTeacherName { get; set; } = string.Empty;
	}
}
