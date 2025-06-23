using EduConnect.Application.DTOs.Responses.StudentResponses;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.Commons;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IStudentService
	{
		Task<BaseResponse<int>> CountStudentsAsync();
		Task<BaseResponse<byte[]>> ExportStudentsToExcelAsync(ExportStudentRequest request);
		Task<PagedResponse<StudentDto>> GetPagedStudentsAsync(StudentPagingRequest request);
		Task<BaseResponse<string>> CreateStudentAsync(CreateStudentRequest request);
		Task<BaseResponse<string>> UpdateStudentAsync(Guid id, UpdateStudentRequest request);
	}
}
