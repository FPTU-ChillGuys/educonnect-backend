using EduConnect.Application.DTOs.Responses.StudentResponses;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IStudentService
	{
		Task<BaseResponse<byte[]>> ExportStudentsToExcelAsync(StudentPagingRequest request);
		Task<PagedResponse<StudentDto>> GetPagedStudentsAsync(StudentPagingRequest request);
		Task<BaseResponse<string>> CreateStudentAsync(CreateStudentRequest request);
		Task<BaseResponse<string>> UpdateStudentAsync(Guid id, UpdateStudentRequest request);

        // Get by search
		Task<BaseResponse<List<StudentDto>>> GetStudentsBySearchAsync(string search);
    }
}
