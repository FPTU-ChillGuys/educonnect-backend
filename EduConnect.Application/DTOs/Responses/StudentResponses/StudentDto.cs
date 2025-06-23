namespace EduConnect.Application.DTOs.Responses.StudentResponses
{
	public class StudentDto
	{
		public Guid StudentId { get; set; }
		public string? FullName { get; set; }
		public string? StudentCode { get; set; }
		public string? Gender { get; set; }
		public string? ClassName { get; set; }
		public string? ParentEmail { get; set; }
	}
}
