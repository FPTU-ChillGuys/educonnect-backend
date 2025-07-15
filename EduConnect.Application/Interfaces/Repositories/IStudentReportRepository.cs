using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IStudentReportRepository
	{
		Task<StudentReport?> GetLatestStudentReportForNotificationAsync(GetStudentReportToNotifyRequest request);
	}
}
