namespace EduConnect.Application.DTOs.Responses.PeriodResponses
{
	public class PeriodLookuptDto
	{
		public Guid PeriodId { get; set; }
		public int PeriodNumber { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
