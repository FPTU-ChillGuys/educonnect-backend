using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Classroom
	{
		[Key]
		public Guid ClassID { get; set; }
		[Required]
		public string ClassName { get; set; } = default!;
		public Guid HomeroomTeacherID { get; set; }
		[Required]
		public string AcademicYear { get; set; } = default!;

		public User? HomeroomTeacher { get; set; }
		public ICollection<Student>? Students { get; set; }
		public ICollection<Schedule>? Schedules { get; set; }
		public ICollection<ClassNotebook>? ClassNotebooks { get; set; }
		public ICollection<Reminder>? Reminders { get; set; }
	}
}
