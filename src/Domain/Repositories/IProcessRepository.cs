using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IProcessRepository
    {
        Task CreateAsync(Process process);
        Task<Process> GetByIdAsync(Guid id);
        IEnumerable<Process> GetAll();
    }
}