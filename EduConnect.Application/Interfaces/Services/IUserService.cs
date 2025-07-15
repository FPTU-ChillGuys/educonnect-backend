using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.Interfaces.Services
{
	public interface IUserService
	{
		Task<PagedResponse<UserDto>> GetPagedUsersAsync(FilterUserRequest request);
		Task<BaseResponse<List<UserLookupDto>>> GetUserLookupAsync(FilterUserRequest request);
		Task<BaseResponse<UserDto>> GetUserByIdAsync(Guid id); 
		Task<BaseResponse<byte[]>> ExportUsersToExcelAsync(FilterUserRequest request);
		Task<BaseResponse<string>> UpdateUserAsync(Guid id, UpdateUserRequest request);
		Task<BaseResponse<string>> UpdateUserStatsusAsync(Guid id, UpdateUserStatusRequest request);
	}
}
