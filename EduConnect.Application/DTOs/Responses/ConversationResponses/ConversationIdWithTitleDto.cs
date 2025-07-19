using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.DTOs.Responses.ConversationResponses
{
    public class ConversationIdWithTitleDto
    {
        public Guid ConversationId { get; set; }
        public string? Title { get; set; }
    }
}
