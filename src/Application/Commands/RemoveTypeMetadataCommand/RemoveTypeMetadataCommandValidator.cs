using FluentValidation;

namespace Application.Commands.RemoveTypeMetadataCommand
{
    public class RemoveTypeMetadataCommandValidator : AbstractValidator<RemoveTypeMetadataCommand>
    {
        public RemoveTypeMetadataCommandValidator()
        {
            RuleFor(x => x.AssemblyFullName)
                .NotEmpty();

            RuleFor(x => x.TypeFullName)
                .NotEmpty();
        }
    }
}