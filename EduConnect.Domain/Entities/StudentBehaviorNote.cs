using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class StudentBehaviorNote
	{
		[Key]
		public Guid NoteId { get; set; }

		[Required]
		public Guid ClassSessionId { get; set; }
		[ForeignKey(nameof(ClassSessionId))]
		public ClassSession ClassSession { get; set; }

		[Required]
		public Guid StudentId { get; set; }
		[ForeignKey(nameof(StudentId))]
		public Student Student { get; set; }

		[Required]
		public string BehaviorType { get; set; }

		public string? Comment { get; set; }

		[Required]
		public DateTime Timestamp { get; set; }
	}
}
