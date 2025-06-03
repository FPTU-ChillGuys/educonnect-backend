using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class StudentLessonNote
	{
		[Key]
		public Guid StudentNoteID { get; set; }
		public Guid LessonLogID { get; set; }
		public Guid StudentID { get; set; }
		public string? NoteContent { get; set; }

		public LessonLog? LessonLog { get; set; }
		public Student? Student { get; set; }
	}
}
