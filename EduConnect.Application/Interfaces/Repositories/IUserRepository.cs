using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<int> CountUsersInRoleAsync(string roleName);
		Task<(IEnumerable<User> Items, int TotalCount)> GetPagedUsersAsync(FilterUserRequest request);
		Task<List<User>> GetUsersForExportAsync(ExportUserRequest request);
	}
}
