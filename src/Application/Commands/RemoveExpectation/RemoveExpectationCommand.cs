using System;
using MediatR;

namespace Application.Commands.RemoveExpectation
{
    public class RemoveExpectationCommand : IRequest<Unit>
    {
        public Guid ExpectationId { get; set; }
    }
}