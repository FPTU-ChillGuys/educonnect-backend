using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<int> CountUsersInRoleAsync(string roleName);
		Task<(User? User, string? RoleName)> GetUserWithRoleByIdAsync(Guid userId);
		Task<(List<UserDto> Items, int TotalCount)> GetPagedUsersAsync(FilterUserRequest request);
		Task<List<UserLookupDto>> GetUserLookupAsync(FilterUserRequest request);
		Task<List<(string DeviceToken, Guid StudentId, Guid UserId)>> GetAllParentDeviceTokensOfActiveStudentsAsync();
		Task<List<UserDto>> GetUsersForExportAsync(FilterUserRequest request);
	}
}
