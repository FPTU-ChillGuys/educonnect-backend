using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class ClassNotebook
	{
		[Key]
		public Guid NotebookID { get; set; }
		public Guid ClassID { get; set; }
		public DateTime Date { get; set; }
		public string? SummaryNote { get; set; }
		public Guid CreatedBy { get; set; }

		public Classroom? Class { get; set; }
		public User? Creator { get; set; }
		public ICollection<LessonLog>? LessonLogs { get; set; }
	}
}
