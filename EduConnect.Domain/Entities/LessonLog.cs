using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class LessonLog
	{
		[Key]
		public Guid LessonLogID { get; set; }
		public Guid NotebookID { get; set; }
		public Guid ScheduleID { get; set; }
		public Guid TeacherID { get; set; }
		[Required]
		public string Content { get; set; } = default!;
		public int TotalPresent { get; set; }
		public int TotalAbsent { get; set; }
		public string? DisciplineNote { get; set; }

		public ClassNotebook? Notebook { get; set; }
		public Schedule? Schedule { get; set; }
		public User? Teacher { get; set; }
		public ICollection<StudentLessonNote>? StudentLessonNotes { get; set; }
	}
}
