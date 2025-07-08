using EduConnect.Application.DTOs.Responses.ClassResponses;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IClassService
	{
		Task<BaseResponse<int>> CountClassesAsync();
		Task<PagedResponse<ClassDto>> GetPagedClassesAsync(ClassPagingRequest request);
		Task<BaseResponse<ClassDto>> GetClassByIdAsync(Guid classId);
		Task<BaseResponse<List<ClassDto>>> GetClassesByTeacherIdAsync(Guid teacherId);
		Task<BaseResponse<List<ClassDto>>> GetClassesByStudentIdAsync(Guid studentId);
		Task<BaseResponse<string>> CreateClassAsync(CreateClassRequest request);
		Task<BaseResponse<string>> UpdateClassAsync(Guid id, UpdateClassRequest request);
		Task<BaseResponse<string>> DeleteClassAsync(Guid classId);
	}
}
