using EduConnect.Application.DTOs.Responses.ClassResponses;
using EduConnect.Application.DTOs.Requests.ClassRequests;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IClassService
	{
		Task<PagedResponse<ClassDto>> GetPagedClassesAsync(ClassPagingRequest request);
		Task<BaseResponse<List<ClassLookupDto>>> GetClassLookupAsync(ClassPagingRequest request);
		Task<BaseResponse<ClassDto>> GetClassByIdAsync(Guid classId);
		Task<BaseResponse<string>> CreateClassAsync(CreateClassRequest request);
		Task<BaseResponse<string>> UpdateClassAsync(Guid id, UpdateClassRequest request);
		Task<BaseResponse<string>> DeleteClassAsync(Guid classId);

        //Get by search 
		Task<BaseResponse<List<ClassDto>>> GetClassesBySearchAsync(string search);
    }
}
