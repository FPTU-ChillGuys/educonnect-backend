﻿namespace EduConnect.Application.Interfaces.Services
{
	public interface IEmailService
	{
		public Task SendEmailAsync(string to, string subject, string htmlBody);
	}
}
