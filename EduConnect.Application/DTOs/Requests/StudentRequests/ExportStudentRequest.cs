namespace EduConnect.Application.DTOs.Requests.StudentRequests
{
	public class ExportStudentRequest
	{
		public string? Keyword { get; set; }
		public Guid? ClassId { get; set; }
		public string? Status { get; set; }
		public string? Gender { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
	}
}
