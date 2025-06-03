using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Student
	{
		[Key]
		public Guid StudentID { get; set; }
		[Required]
		public string FullName { get; set; } = default!;
		public DateTime DOB { get; set; }
		[Required]
		public string Gender { get; set; } = default!;
		public Guid ClassID { get; set; }
		public Guid ParentID { get; set; }

		public Classroom? Class { get; set; }
		public User? Parent { get; set; }
		public ICollection<StudentLessonNote>? LessonNotes { get; set; }
		public ICollection<Notification>? Notifications { get; set; }
	}
}
