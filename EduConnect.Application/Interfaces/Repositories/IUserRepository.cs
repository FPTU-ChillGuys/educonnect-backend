using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<int> CountUsersInRoleAsync(string roleName);
		Task<(List<UserDto> Items, int TotalCount)> GetPagedUsersAsync(FilterUserRequest request);
		Task<(User? User, string? RoleName)> GetUserWithRoleByIdAsync(Guid userId);
		Task<List<UserDto>> GetUsersForExportAsync(ExportUserRequest request);
	}
}
