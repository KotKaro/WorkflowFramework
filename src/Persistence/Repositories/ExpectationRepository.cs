using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate.Expectations;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ExpectationRepository : IExpectationRepository
    {
        private readonly WorkflowFrameworkDbContext _context;

        public ExpectationRepository(WorkflowFrameworkDbContext context)
        {
            _context = context;
        }

        public Task<Expectation> GetByIdAsync(Guid id)
        {
            return _context
                .Set<Expectation>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}