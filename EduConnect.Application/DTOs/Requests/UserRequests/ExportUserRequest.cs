namespace EduConnect.Application.DTOs.Requests.UserRequests
{
	public class ExportUserRequest 
	{
		public string? Keyword { get; set; }
		public string? Role { get; set; }
		public bool? IsHomeroomTeacher { get; set; }
		public bool? IsSubjectTeacher { get; set; }
		public string? Subject { get; set; }
	}
}
