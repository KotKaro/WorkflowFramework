using MediatR;

namespace Application.Commands.CreateStep
{
    public class CreateStepCommand : IRequest
    {
        public string StepName { get; set; }
    }
}