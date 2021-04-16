using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateAsync(Expectation expectation)
        {
            await Context.Set<Expectation>().AddAsync(expectation);
        }

        public IEnumerable<Expectation> GetByIds(IEnumerable<Guid> expectationIds)
        {
            return Context.Set<Expectation>().Where(x => expectationIds.Contains(x.Id));
        }

        public void Remove(Expectation expectation)
        {
            Context.Set<Expectation>().Remove(expectation);
        }
    }
}