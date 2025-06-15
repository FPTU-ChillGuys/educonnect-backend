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
		public Guid ClassId { get; set; }

		[ForeignKey(nameof(ClassId))]
		public Class Class { get; set; }

		[Required]
		public DateTime GeneratedDate { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		[Required]
		public ReportType Type { get; set; } // Enum: Daily, Weekly

		public string SummaryContent { get; set; }

		public bool GeneratedByAI { get; set; }

		public ICollection<Notification> Notifications { get; set; }
	}
}
