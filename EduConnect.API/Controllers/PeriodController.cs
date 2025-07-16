using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PeriodController : ControllerBase
	{
		private readonly IPeriodService _periodService;
		public PeriodController(IPeriodService periodService)
		{
			_periodService = periodService;
		}

		[HttpGet("lookup")]
		public async Task<IActionResult> GetPeriodLookup()
		{
			var result = await _periodService.GetPeriodLookupAsync();
			return result.Success ? Ok(result) : NotFound(result);
		}
	}
}
