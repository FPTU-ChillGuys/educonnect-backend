using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class ClassBehaviorLog
	{
		[Key]
		public Guid LogId { get; set; }

		[Required]
		public Guid ClassSessionId { get; set; }
		[ForeignKey(nameof(ClassSessionId))]
		public ClassSession ClassSession { get; set; }

		[Required]
		public string BehaviorType { get; set; }

		public string? Note { get; set; }

		[Required]
		public DateTime Timestamp { get; set; }
	}
}
