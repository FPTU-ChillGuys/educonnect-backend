namespace EduConnect.Application.DTOs.Requests.SubjectRequests
{
	public class CreateSubjectRequest
	{
		public string SubjectName { get; set; } = string.Empty;
		public string? Description { get; set; }
	}
}
