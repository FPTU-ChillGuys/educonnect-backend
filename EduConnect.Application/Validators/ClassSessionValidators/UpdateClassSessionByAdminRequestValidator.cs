using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassSessionValidators
{
	public class UpdateClassSessionByAdminRequestValidator : AbstractValidator<UpdateClassSessionByAdminRequest>
	{
		public UpdateClassSessionByAdminRequestValidator()
		{
			RuleFor(x => x.ClassId).NotEmpty();
			RuleFor(x => x.SubjectId).NotEmpty();
			RuleFor(x => x.TeacherId).NotEmpty();
			RuleFor(x => x.Date).NotEmpty().GreaterThanOrEqualTo(DateTime.Today.AddDays(-30)); // up to you
			RuleFor(x => x.PeriodNumber).GreaterThan(0);
			RuleFor(x => x.LessonContent).NotEmpty().MaximumLength(500);
			RuleFor(x => x.TotalAbsentStudents).GreaterThanOrEqualTo(0);
		}
	}
}
