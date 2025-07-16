using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class Student
	{
		[Key]
		public Guid StudentId { get; set; }

		[Required]
		public string StudentCode { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		public string? Gender { get; set; }

		public string? AvatarUrl { get; set; }

		[Required]
		public string Status { get; set; } = "Active";

		[Required]
		public Guid ClassId { get; set; }

		[Required]
		public Guid ParentId { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }

		[ForeignKey(nameof(ParentId))]
		public User Parent { get; set; }

		public ICollection<StudentBehaviorNote> BehaviorNotes { get; set; }
		public ICollection<StudentReport> StudentReports { get; set; }
	}
}
