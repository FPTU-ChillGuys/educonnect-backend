using EduConnect.Application.Interfaces.Repositories;
using EduConnect.Application.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EduConnect.Infrastructure.Authorization.Handlers
{
	public class ClassAccessHandler : AuthorizationHandler<ClassAccessRequirement>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IClassRepository _classRepo;

		public ClassAccessHandler(IHttpContextAccessor httpContextAccessor, IClassRepository classRepo)
		{
			_httpContextAccessor = httpContextAccessor;
			_classRepo = classRepo;
		}

		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClassAccessRequirement requirement)
		{
			var user = context.User;
			var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (!Guid.TryParse(userIdStr, out var userId))
				return;

			// Allow Admin or Parent
			if (user.IsInRole("admin") || user.IsInRole("parent"))
			{
				context.Succeed(requirement);
				return;
			}

			// Check if Teacher is homeroom teacher of this class
			if (user.IsInRole("teacher"))
			{
				var routeValues = _httpContextAccessor.HttpContext?.GetRouteData()?.Values;
				if (routeValues != null &&
					routeValues.TryGetValue("id", out var classIdObj) &&
					Guid.TryParse(classIdObj?.ToString(), out var classId))
				{
					var isHomeroomTeacher = await _classRepo.IsHomeroomTeacherAsync(userId, classId);
					if (isHomeroomTeacher)
						context.Succeed(requirement);
				}
			}
		}
	}
}
