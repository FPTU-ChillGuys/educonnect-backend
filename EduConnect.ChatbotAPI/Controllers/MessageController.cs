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



    }
}
