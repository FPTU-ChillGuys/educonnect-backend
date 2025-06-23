namespace EduConnect.Application.DTOs.Requests.StudentRequests
{
	public class StudentPagingRequest
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public string? Keyword { get; set; }
		public Guid? ClassId { get; set; }
		public string? Status { get; set; }
		public string? Gender { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
	}
}
