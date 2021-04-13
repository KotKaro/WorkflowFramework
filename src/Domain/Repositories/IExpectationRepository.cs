using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate.Expectations;

namespace Domain.Repositories
{
    public interface IExpectationRepository
    {
        Task<Expectation> GetByIdAsync(Guid id);
    }
}