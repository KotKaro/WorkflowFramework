using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;

namespace Domain.Services
{
    public class ExpectationResolverService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public ExpectationResolverService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public bool Resolve(IReadOnlyCollection<Expectation> expectations, params Argument[] arguments)
        {
            if (!expectations.Any())
            {
                return true;
            }

            var repository = _repositoryFactory.GetByEntityType(expectations.GetExpectationDescribedType());

            return repository.GetAll().Any(instance =>
            {
                return expectations.All(expectation => expectation.Apply(instance, arguments));
            });
        }
    }
}