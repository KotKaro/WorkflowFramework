using System;
using System.Collections.Generic;
using MediatR;

namespace Application.Commands.AddExpectation.AggregateExpectation
{
    public abstract class AddAggregateExpectationCommand : IRequest
    {
        public Guid StepNavigatorId { get; set; }
        public IEnumerable<Guid> ExpectationIds { get; set; }
    }
}