using EduConnect.Application.DTOs.Requests.StudentRequests;
using FluentValidation;

namespace EduConnect.Application.Validators.StudentValidators
{
	public class GetStudentsByClassIdRequestValidator : AbstractValidator<GetStudentsByClassIdRequest>
	{
		public GetStudentsByClassIdRequestValidator()
		{
			RuleFor(x => x.SortBy)
			.Must(BeAValidSortField)
			.WithMessage("Invalid sort field");
		}

		private bool BeAValidSortField(string? sortBy)
		{
			if (string.IsNullOrWhiteSpace(sortBy)) return true;

			var validFields = new[] { "FullName", "StudentCode", "Gender", "DateOfBirth" };
			return validFields.Contains(sortBy);
		}
	}
}
