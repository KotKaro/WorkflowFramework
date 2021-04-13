using MediatR;

namespace Application.Commands.CreateProcess
{
    public class CreateProcessCommand : IRequest
    {
        public string ProcessName { get; set; }
    }
}