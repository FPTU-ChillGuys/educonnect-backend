using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Domain.Entities;

namespace EduConnect.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<int> CountHomeroomTeachersAsync();
		Task<int> CountSubjectTeachersAsync();
		Task<int> CountUsersInRoleAsync(string roleName);
		Task<(IEnumerable<User> Items, int TotalCount)> GetPagedUsersAsync(UserFilterRequest request);
		Task<List<User>> GetUsersForExportAsync(ExportUserRequest request);
	}
}
