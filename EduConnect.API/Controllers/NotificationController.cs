using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.DTOs.Responses.NotificationResponses;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.NotificationRequests;
using Microsoft.AspNetCore.Authorization;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "admin,parent,teacher")] 
		public async Task<ActionResult<BaseResponse<List<NotificationDto>>>> GetNotificationsByUserId(Guid userId)
        {
            var response = await _notificationService.GetNotificationsByUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPatch("mark-as-read/{notificationId}")]
        public async Task<ActionResult<BaseResponse<NotificationDto>>> MarkNotificationAsRead(Guid notificationId)
        {
            var response = await _notificationService.MarkNotificationAsReadAsync(notificationId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
		}
	}
}
