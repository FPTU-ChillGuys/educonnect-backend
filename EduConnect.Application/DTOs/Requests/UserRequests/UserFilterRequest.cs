namespace EduConnect.Application.DTOs.Requests.UserRequests
{
	public class UserFilterRequest
	{
		public string? Keyword { get; set; }
		public string? Role { get; set; }
		public bool? IsHomeroomTeacher { get; set; }
		public bool? IsSubjectTeacher { get; set; }
		public string? Subject { get; set; }

		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
