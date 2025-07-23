using EduConnect.Application.Commons.Extensions;
using EduConnect.Application.DTOs.Requests.StudentRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.StudentValidators
{
	public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
	{
		public CreateStudentRequestValidator()
		{
			RuleFor(x => x.StudentCode).NotEmpty().MaximumLength(50);
			RuleFor(x => x.FullName).NotEmpty().WithMessage("Please input full name!").Matches(@"^[\p{L} ]{3,50}$").WithMessage("Full name must be 3-50 characters and only contain Vietnamese letters and spaces.");

			RuleFor(x => x.DateOfBirth)
				.LessThan(DateTimeHelper.GetVietnamTime().Date)
				.WithMessage("Date of birth must be before today (Vietnam time).");

			RuleFor(x => x.Gender).MaximumLength(10);
			RuleFor(x => x.ClassId).NotEmpty();
			RuleFor(x => x.ParentId).NotEmpty();
		}
	}
}
