using EduConnect.Application.DTOs.Requests.StudentRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.StudentValidators
{
	public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
	{
		public UpdateStudentRequestValidator()
		{
			RuleFor(x => x.StudentCode).NotEmpty().WithMessage("Student code is required.").MaximumLength(50);
			RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
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
