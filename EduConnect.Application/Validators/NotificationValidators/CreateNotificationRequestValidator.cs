using EduConnect.Application.DTOs.Requests.NotificationRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.NotificationValidators
{
    public class CreateNotificationRequestValidator : AbstractValidator<CreateNotificationRequest>
    {
        public CreateNotificationRequestValidator()
        {
            RuleFor(x => x.RecipientUserId).NotEmpty().WithMessage("RecipientUserId is required.");
            // Optionally, add more rules for ClassReportId/StudentReportId if needed
        }
    }
}