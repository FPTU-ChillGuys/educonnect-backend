using EduConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversationById(Guid id)
        {
            var result = await _conversationService.GetConversationById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetConversationByUserId(Guid id)
        {
            var result = await _conversationService.GetAllConversationsByUserId(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }


    }
}
