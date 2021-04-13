using MediatR;

namespace Application.Commands.CreateTypeMetadata
{
    public class CreateTypeMetadataCommand : IRequest
    {
        public string TypeFullName { get; set; }
        public string AssemblyFullName { get; set; }
    }
}