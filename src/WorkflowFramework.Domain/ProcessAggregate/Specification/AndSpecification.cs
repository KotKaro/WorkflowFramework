using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkflowFramework.Domain.ProcessAggregate.Specification
{
    public class AndSpecification : ISpecification
    {
        private readonly ISpecification[] _specification;

        public AndSpecification(params ISpecification[] specification)
        {
            if (specification?.Length < 2)
            {
                throw new InvalidOperationException("Cannot apply AND specification for less than two specifications.");
            }

            _specification = specification;
        }

        public bool Apply(object obj, IEnumerable<Argument> arguments)
        {
            return _specification.Aggregate(false, (current, specification) => current && specification.Apply(obj, arguments));
        }
    }
}