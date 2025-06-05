using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class ClassNotebook
	{
		[Key]
		public Guid NotebookId { get; set; }

		[Required]
		public Guid ClassPeriodId { get; set; }

		[ForeignKey(nameof(ClassPeriodId))]
		public ClassPeriod ClassPeriod { get; set; }

		[Required]
		public string LessonContent { get; set; }

		public int TotalAbsentStudents { get; set; }

		public string? GeneralBehaviorNote { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public ICollection<ClassBehaviorLog> ClassBehaviorLogs { get; set; }
		public ICollection<StudentBehaviorNote> StudentBehaviorNotes { get; set; }
	}
}
