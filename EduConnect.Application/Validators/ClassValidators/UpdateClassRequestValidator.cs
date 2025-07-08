using EduConnect.Application.DTOs.Requests.ClassRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassValidators
{
	public class UpdateClassRequestValidator : AbstractValidator<UpdateClassRequest>
	{
		public UpdateClassRequestValidator()
		{
			RuleFor(x => x.GradeLevel)
				.NotEmpty()
				.Must(value => value == "10" || value == "11" || value == "12")
				.WithMessage("GradeLevel must be either 10, 11, or 12.");
			RuleFor(x => x.ClassName).NotEmpty().MaximumLength(50);
			RuleFor(x => x.AcademicYear).NotEmpty().Matches(@"^\d{4}-\d{4}$").WithMessage("Format should be YYYY-YYYY");
			RuleFor(x => x.HomeroomTeacherId).NotEmpty();
		}
	}

}
