﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduConnect.Domain.Entities
{
	public class Notification
	{
		[Key]
		public Guid NotificationId { get; set; }

		[Required]
		public Guid RecipientUserId { get; set; }

		[ForeignKey(nameof(RecipientUserId))]
		public User Recipient { get; set; }

		public Guid? ClassReportId { get; set; }
		public Guid? StudentReportId { get; set; }

		[ForeignKey(nameof(ClassReportId))]
		public ClassReport? ClassReport { get; set; }

		[ForeignKey(nameof(StudentReportId))]
		public StudentReport? StudentReport { get; set; }

		[Required]
		public DateTime SentAt { get; set; }

		public bool IsRead { get; set; }	
	}
}
