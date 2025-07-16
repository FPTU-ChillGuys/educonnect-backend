using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class Message
	{
		[Key]
		public Guid MessageId { get; set; }

		[Required]
		public Guid ParentId { get; set; }

		[ForeignKey(nameof(ParentId))]
		public User Parent { get; set; }

		[Required]
		public string Content { get; set; }

		public string? AIResponse { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }
	}
}
