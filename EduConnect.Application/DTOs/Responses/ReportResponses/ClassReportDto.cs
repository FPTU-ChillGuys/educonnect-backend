using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTOs.Responses.ReportResponses
{
	public class ClassReportDto
	{
		public Guid ReportId { get; set; }
		public Guid ClassId { get; set; }
		public DateTime GeneratedDate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public ReportType Type { get; set; }
		public string SummaryContent { get; set; }
		public bool GeneratedByAI { get; set; }
	}
}
