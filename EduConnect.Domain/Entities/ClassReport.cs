using EduConnect.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class ClassReport
	{
		[Key]
		public Guid ReportId { get; set; }

		[Required]
		public Guid ClassroomId { get; set; }

		[ForeignKey(nameof(ClassroomId))]
		public Classroom Classroom { get; set; }

		[Required]
		public DateTime GeneratedDate { get; set; }

		[Required]
		public ReportType Type { get; set; } // Enum: Daily, Weekly

		public string SummaryContent { get; set; }

		public bool GeneratedByAI { get; set; }

		public ICollection<Notification> Notifications { get; set; }
	}
}
