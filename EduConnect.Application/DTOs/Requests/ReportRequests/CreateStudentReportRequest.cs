using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTOs.Requests.ReportRequests
{
	public class CreateStudentReportRequest
	{
		public Guid StudentId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public ReportType Type { get; set; }
		public string SummaryContent { get; set; } = string.Empty;
		public bool GeneratedByAI { get; set; }
	}
}
