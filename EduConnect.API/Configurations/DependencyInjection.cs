using EduConnect.API.Middlewares;

namespace EduConnect.API.Configurations
{
	public static class DependencyInjection
	{
		public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}
