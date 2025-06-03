using EduConnect.API.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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
