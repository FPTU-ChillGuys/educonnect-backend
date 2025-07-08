namespace EduConnect.Application.Interfaces.Services
{
	public interface INotificationJobService
	{
		void SendReportNotificationToParents(string message, string deviceToken);
		void SendTestNotification(string message, string deviceToken);
	}
}
