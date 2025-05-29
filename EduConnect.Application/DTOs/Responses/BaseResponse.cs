namespace EduConnect.Application.DTOs.Responses
{
	public class BaseResponse
	{
		public bool Success { get; set; } = true;
		public string Message { get; set; } = "OK";
		public List<string>? Errors { get; set; }

		public static BaseResponse Ok(string message = "Success")
			=> new() { Success = true, Message = message };

		public static BaseResponse Fail(string message, List<string>? errors = null)
			=> new() { Success = false, Message = message, Errors = errors };
	}

	public class BaseResponse<T> : BaseResponse
	{
		public T? Data { get; set; }

		public static BaseResponse<T> Ok(T data, string message = "Success")
			=> new() { Success = true, Message = message, Data = data };

		public static BaseResponse<T> Fail(string message, List<string>? errors = null)
			=> new() { Success = false, Message = message, Errors = errors };
	}

	public class PagedResponse<T> : BaseResponse<List<T>>
	{
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }

		public static PagedResponse<T> Ok(List<T> data, int total, int page, int pageSize, string message = "Success")
		{
			return new PagedResponse<T>
			{
				Success = true,
				Message = message,
				Data = data,
				TotalCount = total,
				Page = page,
				PageSize = pageSize
			};
		}
	}
}
