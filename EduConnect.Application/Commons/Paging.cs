namespace EduConnect.Application.Commons
{
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
