using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassSessionValidators
{
	public class UpdateClassSessionRequestValidator : AbstractValidator<UpdateClassSessionRequest>
	{
		public UpdateClassSessionRequestValidator()
		{
			RuleFor(x => x.LessonContent).NotEmpty().MaximumLength(500);
			RuleFor(x => x.TotalAbsentStudents).GreaterThanOrEqualTo(0);
		}
	}
}
