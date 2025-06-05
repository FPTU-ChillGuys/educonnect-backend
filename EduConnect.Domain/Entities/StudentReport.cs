using EduConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class StudentReport
	{
		[Key]
		public Guid ReportId { get; set; }

		[Required]
		public Guid StudentId { get; set; }

		[ForeignKey(nameof(StudentId))]
		public Student Student { get; set; }

		[Required]
		public DateTime GeneratedDate { get; set; }

		[Required]
		public ReportType Type { get; set; }

		public string SummaryContent { get; set; }

		public bool GeneratedByAI { get; set; }

		public ICollection<Notification> Notifications { get; set; }
	}
}
