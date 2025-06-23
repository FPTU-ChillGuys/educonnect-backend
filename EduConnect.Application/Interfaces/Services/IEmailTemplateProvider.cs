namespace EduConnect.Application.Interfaces.Services
{
	public interface IEmailTemplateProvider
	{
		string GetRegisterTemplate(string username, string verifyUrl);
		string GetForgotPasswordTemplate(string username, string resetUrl);
	}
}
