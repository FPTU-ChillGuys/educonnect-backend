namespace EduConnect.Application.DTOs.Responses.BehaviorResponses
{
	public class StudentBehaviorNoteDto
	{
		public Guid NoteId { get; set; }
		public Guid StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
		public string BehaviorType { get; set; }
		public string? Comment { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
