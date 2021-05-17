using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IValueProviderRepository
    {
        Task<ValueProvider> GetByIdAsync(Guid id);
    }
}