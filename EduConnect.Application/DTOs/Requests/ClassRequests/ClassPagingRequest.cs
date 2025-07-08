using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.DTOs.Requests.ClassRequests
{
	public class ClassPagingRequest : PagedAndSortedRequest
	{
		public string? Keyword { get; set; }
	}
}
