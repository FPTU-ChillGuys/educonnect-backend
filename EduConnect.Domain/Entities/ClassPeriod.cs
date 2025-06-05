using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class ClassPeriod
	{
		[Key]
		public Guid ClassPeriodId { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public int PeriodNumber { get; set; }

		[Required]
		public Guid ClassroomId { get; set; }

		[Required]
		public Guid SubjectId { get; set; }

		[Required]
		public Guid TeacherId { get; set; }

		[ForeignKey(nameof(ClassroomId))]
		public Classroom Classroom { get; set; }

		[ForeignKey(nameof(SubjectId))]
		public Subject Subject { get; set; }

		[ForeignKey(nameof(TeacherId))]
		public User Teacher { get; set; }

		public ClassNotebook ClassNotebook { get; set; }
	}
}
