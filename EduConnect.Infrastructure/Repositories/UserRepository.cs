using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using EduConnect.Persistence.Data;
using EduConnect.Domain.Entities;

namespace EduConnect.Infrastructure.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(EduConnectDbContext context) : base(context) { }

		public async Task<int> CountHomeroomTeachersAsync()
		{
			return await _context.Users
				.Where(u => u.HomeroomClasses.Any() && u.IsActive)
				.CountAsync();
		}

		public async Task<int> CountSubjectTeachersAsync()
		{
			return await _context.Users
				.Where(u => u.TeachingSessions.Any() && u.IsActive)
				.CountAsync();
		}

		public async Task<int> CountUsersInRoleAsync(string roleName)
		{
			var roleId = await _context.Roles
				.Where(r => r.Name == roleName)
				.Select(r => r.Id)
				.FirstOrDefaultAsync();

			if (roleId == default)
				return 0;

			return await _context.UserRoles
				.CountAsync(ur => ur.RoleId == roleId);
		}

		public async Task<(IEnumerable<User> Items, int TotalCount)> GetPagedUsersAsync(UserFilterRequest request)
		{
			IQueryable<User> query = _context.Users
				.Include(u => u.HomeroomClasses)
				.Include(u => u.TeachingSessions)
				.Where(u => u.IsActive);

			// Filter by Role
			if (!string.IsNullOrWhiteSpace(request.Role))
			{
				var roleId = await _context.Roles
					.Where(r => r.Name == request.Role)
					.Select(r => r.Id)
					.FirstOrDefaultAsync();

				if (roleId != default)
				{
					var userIdsInRole = await _context.UserRoles
						.Where(ur => ur.RoleId == roleId)
						.Select(ur => ur.UserId)
						.ToListAsync();

					query = query.Where(u => userIdsInRole.Contains(u.Id));
				}
				else
				{
					return (Enumerable.Empty<User>(), 0);
				}
			}

			// Filter by keyword
			if (!string.IsNullOrWhiteSpace(request.Keyword))
				query = query.Where(u => u.UserName.Contains(request.Keyword) || u.Email.Contains(request.Keyword));

			if (request.IsHomeroomTeacher == true)
				query = query.Where(u => u.HomeroomClasses.Any());

			if (request.IsSubjectTeacher == true)
				query = query.Where(u => u.TeachingSessions.Any());

			if (!string.IsNullOrWhiteSpace(request.Subject))
				query = query.Where(u => u.TeachingSessions.Any(s => s.Subject.SubjectName == request.Subject));

			var totalCount = await query.CountAsync();
			var result = await query
				.Skip((request.PageNumber - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync();

			return (result, totalCount);
		}

		public async Task<List<User>> GetUsersForExportAsync(ExportUserRequest request)
		{
			IQueryable<User> query = _context.Users
				.Include(u => u.HomeroomClasses)
				.Include(u => u.TeachingSessions)
				.Where(u => u.IsActive);

			// Role filter
			if (!string.IsNullOrWhiteSpace(request.Role))
			{
				var roleId = await _context.Roles
					.Where(r => r.Name == request.Role)
					.Select(r => r.Id)
					.FirstOrDefaultAsync();

				if (roleId != default)
				{
					var userIds = await _context.UserRoles
						.Where(ur => ur.RoleId == roleId)
						.Select(ur => ur.UserId)
						.ToListAsync();

					query = query.Where(u => userIds.Contains(u.Id));
				}
				else
				{
					return new List<User>();
				}
			}

			// Other filters
			if (!string.IsNullOrWhiteSpace(request.Keyword))
				query = query.Where(u => u.UserName.Contains(request.Keyword) || u.Email.Contains(request.Keyword));

			if (request.IsHomeroomTeacher == true)
				query = query.Where(u => u.HomeroomClasses.Any());

			if (request.IsSubjectTeacher == true)
				query = query.Where(u => u.TeachingSessions.Any());

			if (!string.IsNullOrWhiteSpace(request.Subject))
				query = query.Where(u => u.TeachingSessions.Any(s => s.Subject.SubjectName == request.Subject));

			return await query.AsNoTracking().ToListAsync();
		}
	}
}
