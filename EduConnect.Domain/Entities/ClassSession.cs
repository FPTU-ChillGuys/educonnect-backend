using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class ClassSession
	{
		[Key]
		public Guid ClassSessionId { get; set; }

		[Required]
		public Guid ClassId { get; set; }
		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }

		[Required]
		public Guid SubjectId { get; set; }
		[ForeignKey(nameof(SubjectId))]
		public Subject Subject { get; set; }

		[Required]
		public Guid TeacherId { get; set; }
		[ForeignKey(nameof(TeacherId))]
		public User Teacher { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public int PeriodNumber { get; set; }

		[Required]
		public string LessonContent { get; set; }

		public int TotalAbsentStudents { get; set; }

		public string? GeneralBehaviorNote { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public DateTime? DeleteAt { get; set; }

		public bool IsDeleted { get; set; } = false;

		public ICollection<ClassBehaviorLog> ClassBehaviorLogs { get; set; }
		public ICollection<StudentBehaviorNote> StudentBehaviorNotes { get; set; }
	}
}
