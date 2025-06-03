namespace EduConnect.Application.Interfaces.Services
{
	public interface IEmailTemplateProvider
	{
		public string GetRegisterTemplate(string username, string verifyUrl);
	}
}
