using EduConnect.Application.DTOs.Requests.StudentRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.StudentValidators
{
	public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
	{
		public UpdateStudentRequestValidator()
		{
			RuleFor(x => x.StudentCode).NotEmpty().WithMessage("Student code is required.").MaximumLength(50);
			RuleFor(x => x.FullName).NotEmpty().WithMessage("Please input full name!").Matches(@"^[\p{L} ]{3,50}$").WithMessage("Full name must be 3-50 characters and only contain Vietnamese letters and spaces.");
			RuleFor(x => x.DateOfBirth)
				.NotEmpty()
				.LessThan(DateTime.Today)
				.WithMessage("Date of birth must be in the past.");

			RuleFor(x => x.Status)
				.Must(s => s == "Active" || s == "Inactive")
				.WithMessage("Status must be either 'Active' or 'Inactive'.");
		}
	}

}
