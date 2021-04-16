using System;
using MediatR;

namespace Application.Commands.AddExpectation.CompareExpectation
{
    public abstract class AddCompareExpectationCommand : IRequest
    {
        public Guid StepNavigatorId { get; set; }
        public Guid ValueAccessorId { get; set; }
        public object Value { get; set; }
    }
}