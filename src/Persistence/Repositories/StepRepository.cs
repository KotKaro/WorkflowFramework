using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StepRepository : IStepRepository
    {
        private readonly WorkflowFrameworkDbContext _context;

        public StepRepository(WorkflowFrameworkDbContext context)
        {
            _context = context;
        }

        public Task<Step> GetByIdAsync(Guid id)
        {
            return _context
                .Set<Step>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Step> GetByNameAsync(string name)
        {
            return _context
                .Set<Step>()
                .FirstOrDefaultAsync(x => x.Name.Value == name);
        }

        public async Task CreateAsync(Step step)
        {
            await _context.Set<Step>().AddAsync(step);
        }

        public void Remove(Step step)
        {
            _context.Set<Step>().Remove(step);
        }
    }
}