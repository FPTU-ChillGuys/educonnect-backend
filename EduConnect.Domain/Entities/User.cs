using Microsoft.AspNetCore.Identity;

namespace EduConnect.Domain.Entities
{
	public class User : IdentityUser<Guid>
	{
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow.AddDays(7);
		public bool IsActive { get; set; } = true;

		public ICollection<Student>? Students { get; set; }
		public ICollection<Schedule>? Schedules { get; set; }
		public ICollection<ClassNotebook>? CreatedNotebooks { get; set; }
		public ICollection<LessonLog>? LessonLogs { get; set; }
		public ICollection<Reminder>? Reminders { get; set; }
		public ICollection<Message>? SentMessages { get; set; }
		public ICollection<Message>? ReceivedMessages { get; set; }
		public ICollection<Notification>? Notifications { get; set; }
	}
}
