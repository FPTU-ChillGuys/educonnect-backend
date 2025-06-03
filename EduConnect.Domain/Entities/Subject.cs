using System.ComponentModel.DataAnnotations;

namespace EduConnect.Domain.Entities
{
	public class Subject
	{
		[Key]
		public Guid SubjectID { get; set; }
		[Required]
		public string SubjectName { get; set; } = default!;
	}
}
