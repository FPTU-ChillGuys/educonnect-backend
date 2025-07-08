using EduConnect.Application.DTOs.Requests.SubjectRequests;
using EduConnect.Application.DTOs.Responses.SubjectResponses;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface ISubjectService
	{
		Task<PagedResponse<SubjectDto>> GetPagedSubjectsAsync(SubjectPagingRequest request);
		Task<BaseResponse<SubjectDto>> GetSubjectByIdAsync(Guid id);
		Task<BaseResponse<string>> CreateSubjectAsync(CreateSubjectRequest request);
		Task<BaseResponse<string>> UpdateSubjectAsync(Guid id, UpdateSubjectRequest request);
		Task<BaseResponse<string>> DeleteSubjectAsync(Guid id);
	}
}
