using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTOs.Responses.ReportResponses
{
	public class StudentReportDto
	{
		public Guid ReportId { get; set; }
		public Guid StudentId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public ReportType Type { get; set; }
		public string SummaryContent { get; set; }
		public bool GeneratedByAI { get; set; }
		public DateTime GeneratedDate { get; set; }
	}

}
