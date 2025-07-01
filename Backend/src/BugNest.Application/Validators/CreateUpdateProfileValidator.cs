using FluentValidation;
using BugNest.Application.Common.Dtos;

namespace BugNest.Application.UseCases.Users.Validators;

public class CreateUpdateProfileValidator : AbstractValidator<UpdateProfileRequestDto>
{
    public CreateUpdateProfileValidator()
    {
        RuleFor(x => x.Username).MaximumLength(30);
        RuleFor(x => x.FullName).MaximumLength(100);
        RuleFor(x => x.PhoneNumber).MaximumLength(20);
    }
}
