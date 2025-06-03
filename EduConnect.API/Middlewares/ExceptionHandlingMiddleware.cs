using EduConnect.Application.Commons;
using System.Text.Json;

namespace EduConnect.API.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);

				await HandleCommonStatusCodesAsync(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception");

				var (statusCode, title, message) = ex switch
				{
					TaskCanceledException or TimeoutException => (StatusCodes.Status408RequestTimeout, "Request Timeout", "The request has timed out. Please try again later."),
					_ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "Sorry, an internal server error occurred. Kindly try again.")
				};

				_logger.LogWarning("Request resulted in status code {StatusCode}: {Message}", statusCode, message);

				var response = BaseResponse<string>.Fail(message);
				var json = JsonSerializer.Serialize(response);
				context.Response.StatusCode = statusCode;
				if (!context.Response.HasStarted)
				{
					context.Response.ContentType = "application/json";
					await context.Response.WriteAsync(json);
				}
			}
		}

		private async Task HandleCommonStatusCodesAsync(HttpContext context)
		{
			string title = string.Empty;
			string message = string.Empty;
			int statusCode = context.Response.StatusCode;

			switch (statusCode)
			{
				case StatusCodes.Status401Unauthorized:
					title = "Unauthorized";
					message = "You are not authorized to access this resource.";
					break;

				case StatusCodes.Status403Forbidden:
					title = "Forbidden";
					message = "You do not have permission to access this resource.";
					break;

				case StatusCodes.Status429TooManyRequests:
					title = "Too Many Requests";
					message = "You have made too many requests in a short period. Please try again later.";
					break;

				default:
					return; // skip non-handled status codes
			}

			var response = BaseResponse<string>.Fail(message);
			var json = JsonSerializer.Serialize(response);
			if (!context.Response.HasStarted)
			{
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsync(json);
			}
		}
	}
}
