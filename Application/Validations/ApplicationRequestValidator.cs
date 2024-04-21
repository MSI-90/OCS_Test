using Application.Dto.Applications.CreateApplication;
using FluentValidation;
using NotionTestWork.Domain;

namespace Application.Validations;
public class ApplicationRequestValidator : AbstractValidator<CreateApplicationRequest>
{
    public ApplicationRequestValidator()
    {
        RuleFor(x => x.Author).NotNull();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).When(x => x.Name is not null);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(300).When(x => x.Description is not null);
        RuleFor(x => x.Outline).MaximumLength(1000).When(x => x.Outline is not null);
    }
}
