using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Subject
	{
		[Key]
		public Guid SubjectId { get; set; }

		[Required]
		public string SubjectName { get; set; }

		public string? Description { get; set; }

		public ICollection<ClassPeriod> ClassPeriods { get; set; }
	}
}
