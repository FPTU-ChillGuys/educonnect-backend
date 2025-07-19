using AutoMapper;
using EduConnect.Application.DTOs.Requests.ConversationRequests;
using EduConnect.Application.Interfaces.Services;
using EduConnect.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;

        public ConversationController(IConversationService conversationService, IMapper mapper)
        {
            _conversationService = conversationService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversationById(Guid id)
        {
            var result = await _conversationService.GetConversationById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("check/{id}")]
        public async Task<IActionResult> CheckConversationExists(Guid id)
        {
            var result = await _conversationService.CheckConversationExists(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetConversationByUserId(Guid id)
        {
            var result = await _conversationService.GetAllConversationsByUserId(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation(CreateConversationRequest createConversation)
        {
            var conversation = _mapper.Map<Conversation>(createConversation);
            var result = await _conversationService.CreateConversation(conversation);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("ids/user/{userId}")]
        public async Task<IActionResult> GetAllConversationIdsByUserId(Guid userId)
        {
            var result = await _conversationService.GetAllConversationIdByUserIdAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
