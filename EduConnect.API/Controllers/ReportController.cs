using EduConnect.Application.DTOs.Requests.ReportRequests;
using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[ApiController]
	[Route("api/class-reports")]
	public class ClassReportController : ControllerBase
	{
		private readonly IReportService _reportService;

		public ClassReportController(IReportService reportService)
		{
			_reportService = reportService;
		}

		[HttpGet("student/latest")]
		public async Task<IActionResult> GetLatestStudentReport([FromQuery] GetStudentReportToNotifyRequest request)
		{
			var result = await _reportService.GetLatestStudentReportForNotificationAsync(request);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpGet("class/lastest")]
		public async Task<IActionResult> GetLastestClassReport([FromQuery] GetClassReportToNotifyRequest request)
		{
			var result = await _reportService.GetLatestClassReportForNotificationAsync(request);
			return result.Success ? Ok(result) : NotFound(result);
		}

		[HttpPost("student")]
		public async Task<IActionResult> CreateStudentReport([FromBody] CreateStudentReportRequest request)
		{
			var result = await _reportService.CreateStudentReportAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}

		[HttpPost("class")]
		public async Task<IActionResult> Create([FromBody] CreateClassReportRequest request)
		{
			var result = await _reportService.CreateClassReportAsync(request);
			return result.Success ? Ok(result) : BadRequest(result);
		}
	}
}
