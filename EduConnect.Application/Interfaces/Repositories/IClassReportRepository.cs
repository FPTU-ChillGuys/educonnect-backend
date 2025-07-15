using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IClassReportRepository
	{
		Task<ClassReport?> GetLatestClassReportForNotificationAsync(GetClassReportToNotifyRequest request);
	}
}
