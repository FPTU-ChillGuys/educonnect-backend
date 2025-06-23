using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Commons;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IUserService
	{
		Task<BaseResponse<int>> CountTeachersAsync();
		Task<BaseResponse<int>> CountHomeroomTeachersAsync();
		Task<BaseResponse<int>> CountSubjectTeachersAsync();
		Task<BaseResponse<string>> UpdateUserAsync(Guid id, UpdateUserRequest request);
		Task<PagedResponse<UserDto>> GetPagedUsersAsync(UserFilterRequest request);
		Task<BaseResponse<byte[]>> ExportUsersToExcelAsync(ExportUserRequest request);
	}
}
