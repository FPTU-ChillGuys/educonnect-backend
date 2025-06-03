using EduConnect.Application.Interfaces.Services;

namespace EduConnect.Infrastructure.Services
{
	public class MailTemplateProvider : IEmailTemplateProvider
	{
		public string GetRegisterTemplate(string username, string verifyUrl)
		{
			return $@"
			<html>
				<body style='font-family: Arial, sans-serif;'>
					<p>Dear {username},</p>
					<p>Thank you for registering with us! Please click <a href='{verifyUrl}'>here</a> to verify your email address.</p>
					<p>If you did not register, please ignore this email.</p>
					<p>Best regards,<br/>EduConnect Team</p>
				</body>
			</html>";
		}
	}
}
