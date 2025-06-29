using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EduConnect.Domain.Entities
{
	public class Message
	{
		[Key]
		public Guid MessageId { get; set; }

		public Guid ConversationId { get; set; }

        [Required]
        public string? Content { get; set; }

		[Required]
        public string? Role { get; set; } = RoleType.User.ToString(); 

        [Required]
		public DateTime CreatedAt { get; set; }

		[JsonIgnore]
		[ForeignKey(nameof(ConversationId))]
        public Conversation? Conversation { get; set; }
    }

	public enum RoleType
	{
		User,
		Assistant
    }
}
