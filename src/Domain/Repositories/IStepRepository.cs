using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IStepRepository
    {
        Task<Step> GetByIdAsync(Guid id);
        Task<Step> GetByNameAsync(string name);
        Task CreateAsync(Step step);

        void Remove(Step step);
    }
}