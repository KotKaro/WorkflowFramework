using System;

namespace WorkflowFramework.Abstractions
{
    public class Expectation<TTarget, TIdType> where TTarget : IUniqueIdentifiable<TIdType>
    {
        private readonly Func<TTarget, bool> _predicate;
        private readonly TIdType _targetId;

        public Expectation(Func<TTarget, bool> predicate, TIdType targetId)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (targetId is null)
            {
                throw new ArgumentNullException(nameof(targetId));
            }

            _predicate = predicate;
            _targetId = targetId;
        }

        public bool IsSatisfied(IExpectationRepository<TTarget> repository)
        {
            repository.GetById(_targetId)
        }
    }
}