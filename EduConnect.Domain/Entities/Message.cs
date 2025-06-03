using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Message
	{
		[Key]
		public Guid MessageID { get; set; }
		public Guid FromUserID { get; set; }
		public Guid ToUserID { get; set; }
		[Required]
		public string Content { get; set; } = default!;
		public DateTime SentTime { get; set; }
		public bool IsFromAI { get; set; }

		public User? FromUser { get; set; }
		public User? ToUser { get; set; }
	}
}
