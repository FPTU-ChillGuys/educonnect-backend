namespace EduConnect.Application.DTOs.Requests.ClassRequests
{
	public class UpdateClassRequest
	{
		public string GradeLevel { get; set; } = string.Empty;
		public string ClassName { get; set; } = string.Empty;
		public string AcademicYear { get; set; } = string.Empty;
		public Guid HomeroomTeacherId { get; set; }
	}
}
