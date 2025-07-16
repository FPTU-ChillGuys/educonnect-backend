using Microsoft.AspNetCore.Http;

namespace EduConnect.Application.DTOs.Requests.StudentRequests
{
	public class CreateStudentRequest
	{
		public string? StudentCode { get; set; }
		public string? FullName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Gender { get; set; }

		public IFormFile? Avatar { get; set; }
		public Guid ClassId { get; set; }
		public Guid ParentId { get; set; }
	}
}
