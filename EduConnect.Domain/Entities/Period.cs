using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Period
	{
		[Key]
		public Guid PeriodId { get; set; }

		[Required]
		public int PeriodNumber { get; set; } 

		[Required]
		public TimeSpan StartTime { get; set; }

		[Required]
		public TimeSpan EndTime { get; set; }

		public ICollection<ClassSession> ClassSessions { get; set; }
	}
}
