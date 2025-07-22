using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Application.DTOs.Responses.NotificationResponses;
using EduConnect.Application.Commons.Dtos;
using EduConnect.Application.DTOs.Requests.NotificationRequests;

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
        public async Task<ActionResult<BaseResponse<List<NotificationDto>>>> GetNotificationsByUserId(Guid userId)
        {
            var response = await _notificationService.GetNotificationsByUserIdAsync(userId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<NotificationDto>>> CreateNotification([FromBody] CreateNotificationRequest request)
        {
            var response = await _notificationService.CreateNotificationAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
