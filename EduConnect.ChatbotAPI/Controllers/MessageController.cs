using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.ChatbotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(
            IMessageService messageService
        ) : ControllerBase
    {

        [HttpGet("conversation/{conversationId}")]
        public async Task<IActionResult> GetAllMessagesByConversationId(Guid conversationId)
        {
            var result = await messageService.GetAllMessagesByConversationId(conversationId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
