namespace EduConnect.Application.DTOs.Responses.UserResponses
{
	public class UserDto
	{
		public Guid UserId { get; set; }
		public string FullName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public bool IsHomeroomTeacher { get; set; }
		public bool IsSubjectTeacher { get; set; }
	}
}
