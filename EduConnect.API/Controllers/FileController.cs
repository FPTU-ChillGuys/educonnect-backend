using EduConnect.Application.DTOs.Requests.UploadFileRequests;
using EduConnect.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FileController : ControllerBase
	{
		private readonly SupabaseStorageService _storageService;

		public FileController(SupabaseStorageService storageService)
		{
			_storageService = storageService;
		}

		[HttpPost("upload")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Upload([FromForm] UploadFileForm form)
		{
			using var stream = form.File.OpenReadStream();
			var url = await _storageService.UploadFileAsync(stream, form.File.FileName);

			return url != null ? Ok(new { url }) : StatusCode(500, "Upload failed.");
		}
	}
}