using EduConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect.Application.DTOs.Requests.ConversationRequests
{
    public class CreateConversationRequest
    {
        public Guid ParentId { get; set; }
        public string? Title { get; set; } = "New Conversation";
        public ICollection<MessageRequest> Messages { get; set; } = new List<MessageRequest>
        {
            new MessageRequest
            {
                MessageId = Guid.NewGuid(),
                Content = "Welcome to the conversation!",
                Role = RoleType.User.ToString(),
                CreatedAt = DateTime.UtcNow
            }
        };


    }

    public class MessageRequest
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Role { get; set; } = RoleType.User.ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
