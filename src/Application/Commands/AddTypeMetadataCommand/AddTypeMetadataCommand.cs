using MediatR;

namespace Application.Commands.AddTypeMetadataCommand
{
    public class AddTypeMetadataCommand : IRequest
    {
        public string TypeFullName { get; set; }
        public string AssemblyFullName { get; set; }
    }
}