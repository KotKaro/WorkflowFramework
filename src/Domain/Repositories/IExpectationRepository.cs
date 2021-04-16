using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ProcessAggregate.Expectations;

namespace Domain.Repositories
{
    public interface IExpectationRepository
    {
        Task<Expectation> GetByIdAsync(Guid id);
        Task CreateAsync(Expectation expectation);
        IEnumerable<Expectation> GetByIds(IEnumerable<Guid> expectationIds);
        void Remove(Expectation expectation);
    }
}