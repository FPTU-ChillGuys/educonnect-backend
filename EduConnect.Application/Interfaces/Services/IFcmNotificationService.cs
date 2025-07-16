namespace EduConnect.Application.Interfaces.Services
{
	public interface IFcmNotificationService
	{
		Task SendNotificationAsync(string deviceToken, string title, string body);
	}
}
