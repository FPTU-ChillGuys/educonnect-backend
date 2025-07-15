using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTOs.Requests.ReportRequests
{
	public class GetStudentReportToNotifyRequest
	{
		public Guid StudentId { get; set; }
		public ReportType? Type { get; set; }           // Optional
		public bool? GeneratedByAI { get; set; }        // Optional
	}
}
