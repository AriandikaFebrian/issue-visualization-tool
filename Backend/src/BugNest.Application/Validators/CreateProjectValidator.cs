using BugNest.Application.Common.Dtos;
using FluentValidation;

namespace BugNest.Application.UseCases.Projects.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nama project wajib diisi.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Visibility)
            .IsInEnum().WithMessage("Tipe visibility tidak valid.");

        // Optional: validasi URL kalau mau
        RuleFor(x => x.RepositoryUrl)
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.RepositoryUrl))
            .WithMessage("Repository URL tidak valid.");

        RuleFor(x => x.DocumentationUrl)
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.DocumentationUrl))
            .WithMessage("Documentation URL tidak valid.");
    }

    private bool BeValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
