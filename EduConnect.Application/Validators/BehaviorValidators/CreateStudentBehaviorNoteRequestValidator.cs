using EduConnect.Application.DTOs.Requests.BehaviorRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.BehaviorValidators
{
	public class CreateStudentBehaviorNoteRequestValidator : AbstractValidator<CreateStudentBehaviorNoteRequest>
	{
		public CreateStudentBehaviorNoteRequestValidator()
		{
			RuleFor(x => x.ClassSessionId).NotEmpty();
			RuleFor(x => x.StudentId).NotEmpty();
			RuleFor(x => x.BehaviorType).NotEmpty().MaximumLength(100);
		}
	}
}
