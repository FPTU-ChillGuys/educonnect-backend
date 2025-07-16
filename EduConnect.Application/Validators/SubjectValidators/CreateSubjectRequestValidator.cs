using EduConnect.Application.DTOs.Requests.SubjectRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.SubjectValidators
{
	public class CreateSubjectRequestValidator : AbstractValidator<CreateSubjectRequest>
	{
		public CreateSubjectRequestValidator()
		{
			RuleFor(x => x.SubjectName)
				.NotEmpty().WithMessage("Subject name is required");
		}
	}
}
