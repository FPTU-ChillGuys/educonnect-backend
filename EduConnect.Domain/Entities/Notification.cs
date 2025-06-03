using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Notification
	{
		[Key]
		public Guid NotificationID { get; set; }
		public Guid StudentID { get; set; }
		public Guid SentBy { get; set; }
		[Required]
		public string Type { get; set; } = default!; // Daily or Weekly
		[Required]
		public string Content { get; set; } = default!;
		public DateTime SentDate { get; set; }

		public Student? Student { get; set; }
		public User? Sender { get; set; }
	}
}
