using FluentValidation;
using BugNest.Application.DTOs.Tags;

namespace BugNest.Application.Validators;

public class CreateTagValidator : AbstractValidator<CreateTagDto>
{
    public CreateTagValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Color).NotEmpty();
    }
}
