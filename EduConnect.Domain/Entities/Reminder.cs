using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Reminder
	{
		[Key]
		public Guid ReminderID { get; set; }
		public Guid ClassID { get; set; }
		public DateTime Date { get; set; }
		[Required]
		public string Content { get; set; } = default!;
		public Guid CreatedBy { get; set; }

		public Classroom? Class { get; set; }
		public User? Creator { get; set; }
	}
}
