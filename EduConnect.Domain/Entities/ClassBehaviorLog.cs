using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class ClassBehaviorLog
	{
		[Key]
		public Guid LogId { get; set; }

		[Required]
		public Guid NotebookId { get; set; }

		[ForeignKey(nameof(NotebookId))]
		public ClassNotebook Notebook { get; set; }

		[Required]
		public string BehaviorType { get; set; }

		public string? Note { get; set; }

		[Required]
		public DateTime Timestamp { get; set; }
	}
}
