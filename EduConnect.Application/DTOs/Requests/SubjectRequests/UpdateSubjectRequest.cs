namespace EduConnect.Application.DTOs.Requests.SubjectRequests
{
	public class UpdateSubjectRequest
	{
		public string SubjectName { get; set; } = string.Empty;
		public string? Description { get; set; }
	}
}
