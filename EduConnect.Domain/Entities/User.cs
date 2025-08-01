﻿using Microsoft.AspNetCore.Identity;

namespace EduConnect.Domain.Entities
{
	public class User : IdentityUser<Guid>
	{
		public string FullName { get; set; } = string.Empty;
		public string? Address { get; set; }
		public string RefreshToken { get; set; } = string.Empty;
		public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow.AddDays(7);
		public string? DeviceToken { get; set; }
		public bool IsActive { get; set; } = true;
		public virtual ICollection<Student> Children { get; set; } // if Parent
		public virtual ICollection<Class> HomeroomClasses { get; set; } // if Teacher
		public virtual ICollection<ClassSession> TeachingSessions { get; set; } // if Teacher
		public virtual ICollection<Notification> Notifications { get; set; } // if Parent
		public virtual ICollection<Message> Messages { get; set; } // if Parent
	}
}
