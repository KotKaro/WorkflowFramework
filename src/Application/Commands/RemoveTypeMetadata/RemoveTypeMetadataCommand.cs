using MediatR;

namespace Application.Commands.RemoveTypeMetadata
{
    public class RemoveTypeMetadataCommand : IRequest
    {
        public string TypeFullName { get; set; }
        public string AssemblyFullName { get; set; }
    }
}