using FluentValidation;

namespace Application.Commands.RemoveTypeMetadata
{
    public class RemoveTypeMetadataCommandValidator : AbstractValidator<RemoveTypeMetadataCommand>
    {
        public RemoveTypeMetadataCommandValidator()
        {
            RuleFor(x => x.AssemblyFullName)
                .NotEmpty();

            RuleFor(x => x.TypeFullName)
                .NotEmpty();

            RuleFor(x => x)
                .Must(x => x.FindType() != null);
        }
    }
}