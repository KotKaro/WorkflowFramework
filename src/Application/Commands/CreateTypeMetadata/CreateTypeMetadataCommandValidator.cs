using FluentValidation;

namespace Application.Commands.CreateTypeMetadata
{
    public class CreateTypeMetadataCommandValidator : AbstractValidator<CreateTypeMetadataCommand>
    {
        public CreateTypeMetadataCommandValidator()
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