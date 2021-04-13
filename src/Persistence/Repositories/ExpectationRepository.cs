using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate.Expectations;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class ExpectationRepository : RepositoryBase, IExpectationRepository
    {
        public ExpectationRepository(WorkflowFrameworkDbContext context) : base(context)
        {
        }

        public Task<Expectation> GetByIdAsync(Guid id)
        {
            return Context
                .Set<Expectation>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}