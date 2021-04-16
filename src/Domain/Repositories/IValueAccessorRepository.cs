using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IValueAccessorRepository
    {
        Task<ValueAccessor> GetByIdAsync(Guid id);
    }
}