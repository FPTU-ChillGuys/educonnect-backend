using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.DTOs.Responses.ReportResponses;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IReportService
	{
		Task<BaseResponse<ClassReportDto>> GetLatestClassReportForNotificationAsync(GetClassReportToNotifyRequest request);
		Task<BaseResponse<StudentReportDto>> GetLatestStudentReportForNotificationAsync(GetStudentReportToNotifyRequest request);
		Task<BaseResponse<string>> CreateClassReportAsync(CreateClassReportRequest request);
		Task<BaseResponse<string>> CreateStudentReportAsync(CreateStudentReportRequest request);
	}
}
