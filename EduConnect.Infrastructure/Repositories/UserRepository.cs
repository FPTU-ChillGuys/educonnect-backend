using EduConnect.Application.DTOs.Requests.UserRequests;
using EduConnect.Application.DTOs.Responses.UserResponses;
using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Domain.Entities;
using EduConnect.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.Infrastructure.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(EduConnectDbContext context) : base(context) { }

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

		public async Task<(List<UserDto> Items, int TotalCount)> GetPagedUsersAsync(FilterUserRequest request)
		{
			var query = BuildUserQuery(request);

			var totalCount = await query.CountAsync();

			var users = await query
				.Skip((request.PageNumber - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(x => new UserDto
				{
					UserId = x.User.Id,
					RoleName = x.RoleName,
					FullName = x.User.FullName,
					Email = x.User.Email,
					PhoneNumber = x.User.PhoneNumber,
					Address = x.User.Address,
					IsActive = x.User.IsActive,
					IsHomeroomTeacher = x.User.HomeroomClasses.Any(),
					IsSubjectTeacher = x.User.TeachingSessions.Any()
				})
				.ToListAsync();

			return (users, totalCount);
		}

		public async Task<List<UserLookupDto>> GetUserLookupAsync(FilterUserRequest request)
		{
			var query = BuildUserQuery(request);

			var users = await query
				.Where(x => x.User.IsActive) 
				.Select(x => new UserLookupDto
				{
					UserId = x.User.Id,
					FullName = x.User.FullName,
					Email = x.User.Email
				})
				.OrderBy(x => x.FullName)
				.ToListAsync();

			return users;
		}

		public async Task<(User? User, string? RoleName)> GetUserWithRoleByIdAsync(Guid userId)
		{
			var user = await _context.Users
				.Include(u => u.HomeroomClasses)
				.Include(u => u.TeachingSessions)
				.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
				return (null, null);

			var roleName = await _context.UserRoles
				.Where(ur => ur.UserId == userId)
				.Join(_context.Roles,
					ur => ur.RoleId,
					r => r.Id,
					(ur, r) => r.Name)
				.FirstOrDefaultAsync();

			return (user, roleName);
		}

		public async Task<List<UserDto>> GetUsersForExportAsync(FilterUserRequest request)
		{
			var query = BuildUserQuery(request);

			var users = await query
				.AsNoTracking()
				.Select(x => new UserDto
				{
					UserId = x.User.Id,
					RoleName = x.RoleName,
					FullName = x.User.FullName,
					Email = x.User.Email,
					PhoneNumber = x.User.PhoneNumber,
					Address = x.User.Address,
					IsActive = x.User.IsActive,
					IsHomeroomTeacher = x.User.HomeroomClasses.Any(),
					IsSubjectTeacher = x.User.TeachingSessions.Any()
				})
				.OrderBy(x => x.FullName)
				.ToListAsync();

			return users;
		}

		private IQueryable<UserWithRole> BuildUserQuery(FilterUserRequest request)
		{
			var query = from u in _context.Users
						select new UserWithRole
						{
							User = u,
							RoleName = (from ur in _context.UserRoles
										join r in _context.Roles on ur.RoleId equals r.Id
										where ur.UserId == u.Id
										select r.Name).FirstOrDefault()
						};

			if (request.StudentId.HasValue)
			{
				query = query.Where(x =>
					x.RoleName == "Parent" &&
					x.User.Children.Any(s => s.StudentId == request.StudentId.Value));
			}

			if (!string.IsNullOrWhiteSpace(request.Role))
				query = query.Where(x => x.RoleName == request.Role);

			if (!string.IsNullOrWhiteSpace(request.Keyword))
				query = query.Where(x =>
					x.User.FullName.Contains(request.Keyword) ||
					x.User.Email.Contains(request.Keyword));

			if (request.IsHomeroomTeacher == true)
				query = query.Where(x => x.User.HomeroomClasses.Any());

			if (request.IsSubjectTeacher == true)
				query = query.Where(x => x.User.TeachingSessions.Any());

			if (!string.IsNullOrWhiteSpace(request.Subject))
				query = query.Where(x => x.User.TeachingSessions.Any(s => s.Subject.SubjectName == request.Subject));

			if (request.IsActive.HasValue)
				query = query.Where(x => x.User.IsActive == request.IsActive.Value);

			if (!string.IsNullOrWhiteSpace(request.Address))
				query = query.Where(x => x.User.Address.Contains(request.Address));

			return query;
		}

		private class UserWithRole
		{
			public User User { get; set; } = default!;
			public string? RoleName { get; set; }
		}

		public async Task<List<(string DeviceToken, Guid StudentId)>> GetAllParentDeviceTokensOfActiveStudentsAsync()
		{
			var parentTokens = await (
				from user in _context.Users
				join userRole in _context.UserRoles on user.Id equals userRole.UserId
				join role in _context.Roles on userRole.RoleId equals role.Id
				where role.Name == "Parent"
					  && user.DeviceToken != null
					  && user.IsActive
				from child in user.Children
				where child.Status == "Active"
				select new
				{
					user.DeviceToken,
					StudentId = child.StudentId
				}
			).ToListAsync();

			return parentTokens
				.Select(x => (x.DeviceToken!, x.StudentId))
				.ToList();
		}
	}
}
