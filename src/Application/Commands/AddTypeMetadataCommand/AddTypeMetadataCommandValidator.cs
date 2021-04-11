using FluentValidation;

namespace Application.Commands.AddTypeMetadataCommand
{
    public class AddTypeMetadataCommandValidator : AbstractValidator<AddTypeMetadataCommand>
    {
        public AddTypeMetadataCommandValidator()
        {
            RuleFor(x => x.AssemblyFullName)
                .NotEmpty();

            RuleFor(x => x.TypeFullName)
                .NotEmpty();
        }
    }
}