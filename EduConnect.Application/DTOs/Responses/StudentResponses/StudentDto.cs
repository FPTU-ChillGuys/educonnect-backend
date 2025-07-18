﻿namespace EduConnect.Application.DTOs.Responses.StudentResponses
{
	public class StudentDto
	{
		public Guid StudentId { get; set; }
		public string? FullName { get; set; }
		public string? StudentCode { get; set; }
		public string? Gender { get; set; }
		public string? AvatarUrl { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? ClassName { get; set; }
		public Guid ParentId { get; set; }
		public string? ParentEmail { get; set; }
		public string? ParentFullName { get; set; }
		public string? ParentPhoneNumber { get; set; }
		public string? Status { get; set; }
	}
}
