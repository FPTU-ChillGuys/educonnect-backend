namespace EduConnect.Application.DTOs.Responses.SubjectResponses
{
	public class SubjectDto
	{
		public Guid SubjectId { get; set; }
		public string SubjectName { get; set; } = string.Empty;
		public string? Description { get; set; }
	}
}
