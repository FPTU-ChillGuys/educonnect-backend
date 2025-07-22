using System.ComponentModel.DataAnnotations;

namespace EduConnect.Application.DTOs.Requests.NotificationRequests
{
    public class CreateNotificationRequest
    {
        [Required]
        public Guid RecipientUserId { get; set; }

        public Guid? ClassReportId { get; set; }
        public Guid? StudentReportId { get; set; }

        // Optionally allow setting IsRead, but default to false
        public bool IsRead { get; set; } = false;
    }
}