using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly WorkflowFrameworkDbContext _context;

        public ProcessRepository(WorkflowFrameworkDbContext context)
        {
            _context = context;
        }
        
        public async Task CreateAsync(Process process)
        {
            await _context.Set<Process>().AddAsync(process);
        }

        public Task<Process> GetByIdAsync(Guid id)
        {
            return _context.Set<Process>()
                .Include(x => x.Steps)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}