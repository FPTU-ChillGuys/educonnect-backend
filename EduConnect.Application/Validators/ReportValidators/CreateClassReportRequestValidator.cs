using EduConnect.Application.DTOs.Requests.ReportRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ReportValidators
{
	public class CreateClassReportRequestValidator : AbstractValidator<CreateClassReportRequest>
	{
		public CreateClassReportRequestValidator()
		{
			RuleFor(x => x.ClassId).NotEmpty();
			RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate);
			RuleFor(x => x.SummaryContent).NotEmpty().MaximumLength(1000);
			RuleFor(x => x.Type).IsInEnum();
		}
	}
}
