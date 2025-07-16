﻿namespace EduConnect.Application.DTOs.Responses.UserResponses
{
	public class UserLookupDto
	{
		public Guid UserId { get; set; }
		public string FullName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
	}
}
