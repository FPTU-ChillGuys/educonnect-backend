using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.UploadFileRequests
{
	public class UploadFileForm
	{
		public string? Title { get; set; }

		[Required]
		public IFormFile File { get; set; }
	}
}
