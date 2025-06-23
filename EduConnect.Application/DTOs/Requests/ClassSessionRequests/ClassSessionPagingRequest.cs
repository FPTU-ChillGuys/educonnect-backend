namespace EduConnect.Application.DTOs.Requests.ClassSessionRequests
{
	public class ClassSessionPagingRequest
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public Guid? ClassId { get; set; }
		public Guid? SubjectId { get; set; }
		public Guid? TeacherId { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
	}
}
