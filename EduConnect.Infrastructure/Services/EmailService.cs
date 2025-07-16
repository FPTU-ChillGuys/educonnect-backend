using EduConnect.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace EduConnect.Infrastructure.Services
{
	public class EmailService(IConfiguration config) : IEmailService
	{
		public async Task SendEmailAsync(string to, string subject, string htmlBody)
		{
			var email = new MimeMessage();
			email.From.Add(new MailboxAddress("StudeeHub", config["EmailAccount:SmtpUser"]));
			email.To.Add(MailboxAddress.Parse(to));
			email.Subject = subject;
			email.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

			using var smtp = new SmtpClient();
			await smtp.ConnectAsync(
				config["EmailSettings:SmtpServer"],
				int.Parse(config["EmailSettings:SmtpPort"]!),
				MailKit.Security.SecureSocketOptions.StartTls
			);
			await smtp.AuthenticateAsync(
				config["EmailAccount:SmtpUser"],
				config["EmailAccount:SmtpPass"]
			);
			await smtp.SendAsync(email);
			await smtp.DisconnectAsync(true);
		}
	}
}
