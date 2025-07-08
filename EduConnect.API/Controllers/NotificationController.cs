using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly IFcmNotificationService _fcm;

		public NotificationController(IFcmNotificationService fcm)
		{
			_fcm = fcm;
		}

		[HttpPost("test")]
		public async Task<IActionResult> TestNotification([FromBody] TestNotificationRequest request)
		{
			await _fcm.SendNotificationAsync(request.DeviceToken, request.Title, request.Body);
			return Ok("Notification sent.");
		}
	}
	public class TestNotificationRequest
	{
		public string DeviceToken { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
	}
}
