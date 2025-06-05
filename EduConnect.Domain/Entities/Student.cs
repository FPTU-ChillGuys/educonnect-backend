using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class Student
	{
		[Key]
		public Guid StudentId { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		public string? Gender { get; set; }

		[Required]
		public Guid ClassroomId { get; set; }

		[Required]
		public Guid ParentId { get; set; }

		[ForeignKey(nameof(ClassroomId))]
		public Classroom Classroom { get; set; }

		[ForeignKey(nameof(ParentId))]
		public User Parent { get; set; }

		public ICollection<StudentBehaviorNote> BehaviorNotes { get; set; }
		public ICollection<StudentReport> StudentReports { get; set; }
	}
}
