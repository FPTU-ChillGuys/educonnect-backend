using EduConnect.Application.DTOs.Requests.ClassSessionRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.ClassSessionValidators
{
    public class ClassSessionPagingRequestValidator : AbstractValidator<ClassSessionPagingRequest>
    {
        public ClassSessionPagingRequestValidator()
        {
            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(x => x.ToDate)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue)
                .WithMessage("FromDate must be less than or equal to ToDate.");
        }
    }
}