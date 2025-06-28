using EduConnect.Application.Commons.Dtos;

namespace EduConnect.Application.DTOs.Requests.SubjectRequests
{
	public class SubjectPagingRequest : PagedAndSortedRequest
	{
		public string? Keyword { get; set; }
	}
}
