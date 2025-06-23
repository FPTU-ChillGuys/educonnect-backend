namespace EduConnect.Application.DTOs.Requests.ClassRequests
{
	public class CreateClassRequest
	{
		public string GradeLevel { get; set; } = default!;
		public string ClassName { get; set; } = default!;
		public string AcademicYear { get; set; } = default!;
		public Guid HomeroomTeacherId { get; set; }
	}
}
