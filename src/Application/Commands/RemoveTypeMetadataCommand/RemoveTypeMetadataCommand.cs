using MediatR;

namespace Application.Commands.RemoveTypeMetadataCommand
{
    public class RemoveTypeMetadataCommand : IRequest
    {
        public string TypeFullName { get; set; }
        public string AssemblyFullName { get; set; }
    }
}