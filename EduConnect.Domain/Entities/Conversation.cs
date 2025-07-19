using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduConnect.Domain.Entities
{
    public class Conversation
    {
        [Key]
        public Guid ConversationId { get; set; }
        public Guid? ParentId { get; set; } 
        public string? Title { get; set; } = "New Conversation";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        [ForeignKey(nameof(ParentId))]
        [JsonIgnore]
        public User? Parent { get; set; }

    }
}
