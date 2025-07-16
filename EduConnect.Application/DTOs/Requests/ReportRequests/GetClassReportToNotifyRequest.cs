using EduConnect.Domain.Enums;

namespace EduConnect.Application.DTOs.Requests.ReportRequests
{
	public class GetClassReportToNotifyRequest
	{
		public Guid ClassId { get; set; }

		public ReportType? Type { get; set; } // Optional: filter by Daily / Weekly

		public bool? GeneratedByAI { get; set; } // Optional
	}
}
