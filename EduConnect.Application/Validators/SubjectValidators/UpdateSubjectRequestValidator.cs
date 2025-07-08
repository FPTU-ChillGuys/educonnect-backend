using EduConnect.Application.DTOs.Requests.SubjectRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.SubjectValidators
{
	public class UpdateSubjectRequestValidator : AbstractValidator<UpdateSubjectRequest>
	{
		public UpdateSubjectRequestValidator()
		{
			RuleFor(x => x.SubjectName)
				.NotEmpty().WithMessage("Subject name is required");
		}
	}
}
