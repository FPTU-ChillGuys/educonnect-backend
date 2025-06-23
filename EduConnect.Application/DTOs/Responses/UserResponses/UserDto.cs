namespace EduConnect.Application.DTOs.Responses.UserResponses
{
	public class UserDto
	{
		public Guid UserId { get; set; }
		public string FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public bool IsHomeroomTeacher { get; set; }
		public bool IsSubjectTeacher { get; set; }
	}
}
