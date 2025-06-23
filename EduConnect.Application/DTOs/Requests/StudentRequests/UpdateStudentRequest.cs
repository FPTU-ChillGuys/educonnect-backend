using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.StudentRequests
{
	public class UpdateStudentRequest
	{
		[Required]
		public string StudentCode { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		public string? Gender { get; set; }

		[Required]
		[RegularExpression("Active|Inactive", ErrorMessage = "Status must be either 'Active' or 'Inactive'.")]
		public string Status { get; set; } = "Active";

		[Required]
		public Guid ClassId { get; set; }

		[Required]
		public Guid ParentId { get; set; }
	}
}
