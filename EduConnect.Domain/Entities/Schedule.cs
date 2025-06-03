using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Schedule
	{
		[Key]
		public Guid ScheduleID { get; set; }
		public Guid ClassID { get; set; }
		public Guid SubjectID { get; set; }
		public Guid TeacherID { get; set; }
		public DateTime Date { get; set; }
		[Required]
		public string Period { get; set; } = default!;

		public Classroom? Class { get; set; }
		public Subject? Subject { get; set; }
		public User? Teacher { get; set; }
	}
}
