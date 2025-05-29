using EduConnect.Api.Middlewares;

namespace EduConnect.API.Extensions
{
	public static class MiddlewareExtensions
	{
		public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}
