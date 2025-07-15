using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassSessionValidators
{
	public class CreateClassSessionRequestValidator : AbstractValidator<CreateClassSessionRequest>
	{
		public CreateClassSessionRequestValidator()
		{
			RuleFor(x => x.ClassId).NotEmpty();
			RuleFor(x => x.SubjectId).NotEmpty();
			RuleFor(x => x.TeacherId).NotEmpty();
			RuleFor(x => x.Date).GreaterThanOrEqualTo(DateTime.Today.AddDays(-1)); // allow current or past 1 day
			RuleFor(x => x.PeriodId).NotEmpty();
			RuleFor(x => x.LessonContent).NotEmpty().MaximumLength(500);
		}
	}
}
