using EduConnect.Application.DTOs.Requests.ReportRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ReportValidators
{
	public class CreateStudentReportRequestValidator : AbstractValidator<CreateStudentReportRequest>
	{
		public CreateStudentReportRequestValidator()
		{
			RuleFor(x => x.StudentId).NotEmpty();
			RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate);
			RuleFor(x => x.Type).IsInEnum();
			RuleFor(x => x.SummaryContent).NotEmpty().MaximumLength(2000);
		}
	}
}
