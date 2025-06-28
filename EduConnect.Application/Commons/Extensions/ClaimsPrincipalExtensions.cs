using System.Security.Claims;

namespace EduConnect.Application.Commons.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static Guid GetUserId(this ClaimsPrincipal user)
		{
			var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);

			if (Guid.TryParse(userIdString, out var userId))
				return userId;

			throw new UnauthorizedAccessException("Invalid or missing user ID in token.");
		}
	}
}
