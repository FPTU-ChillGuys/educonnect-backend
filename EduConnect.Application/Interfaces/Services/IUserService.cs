using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IUserService
	{
		Task<BaseResponse<int>> CountTeachersAsync();
		Task<BaseResponse<int>> CountHomeroomTeachersAsync();
		Task<BaseResponse<int>> CountSubjectTeachersAsync();
		Task<PagedResponse<UserDto>> GetPagedUsersAsync(UserFilterRequest request);
		Task<BaseResponse<byte[]>> ExportUsersToExcelAsync(ExportUserRequest request);
		Task<BaseResponse<string>> UpdateUserAsync(Guid id, UpdateUserRequest request);
		Task<BaseResponse<string>> UpdateUserStatsusAsync(Guid id, UpdateUserStatusRequest request);
	}
}
