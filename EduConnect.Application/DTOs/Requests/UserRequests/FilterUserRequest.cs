using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.DTOs.Requests.UserRequests
{
	public class FilterUserRequest : PagedAndSortedRequest
	{
		public string? Keyword { get; set; }
		public string? Role { get; set; }
		public Guid? StudentId { get; set; }
		public bool? IsHomeroomTeacher { get; set; }
		public bool? IsSubjectTeacher { get; set; }
		public string? Subject { get; set; }
		public bool? IsActive { get; set; }
		public string? Address { get; set; }
	}
}
