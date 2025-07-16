using EduConnect.Application.DTOs.Requests.StudentRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.StudentValidators
{
	public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
	{
		public CreateStudentRequestValidator()
		{
			RuleFor(x => x.StudentCode).NotEmpty().MaximumLength(50);
			RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
			RuleFor(x => x.DateOfBirth).LessThan(DateTime.Today);
			RuleFor(x => x.Gender).MaximumLength(10);
			RuleFor(x => x.ClassId).NotEmpty();
			RuleFor(x => x.ParentId).NotEmpty();
		}
	}
}
