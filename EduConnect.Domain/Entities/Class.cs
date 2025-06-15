using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class Class
	{
		[Key]
		public Guid ClassId { get; set; }

		[Required]
		public string ClassName { get; set; }

		[Required]
		public string AcademicYear { get; set; }

		[Required]
		public Guid HomeroomTeacherId { get; set; }
		[ForeignKey(nameof(HomeroomTeacherId))]
		public User HomeroomTeacher { get; set; }

		public ICollection<Student> Students { get; set; }
		public ICollection<ClassSession> ClassSessions { get; set; }
		public ICollection<ClassReport> ClassReports { get; set; }
	}
}
