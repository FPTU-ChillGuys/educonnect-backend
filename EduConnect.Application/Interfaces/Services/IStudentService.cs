using EduConnect.Application.DTOs.Responses.StudentResponses;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IStudentService
	{
		Task<BaseResponse<int>> CountStudentsAsync();
		Task<BaseResponse<byte[]>> ExportStudentsToExcelAsync(ExportStudentRequest request);
		Task<PagedResponse<StudentDto>> GetStudentsByClassIdAsync(Guid classId, GetStudentsByClassIdRequest request);
		Task<BaseResponse<List<StudentDto>>> GetStudentsByParentIdAsync(Guid parentId);
		Task<PagedResponse<StudentDto>> GetPagedStudentsAsync(StudentPagingRequest request);
		Task<BaseResponse<string>> CreateStudentAsync(CreateStudentRequest request);
		Task<BaseResponse<string>> UpdateStudentAsync(Guid id, UpdateStudentRequest request);
	}
}
