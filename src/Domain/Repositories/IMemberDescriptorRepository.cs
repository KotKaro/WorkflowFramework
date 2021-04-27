using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IMemberDescriptorRepository
    {
        Task<MemberDescriptor> GetByIdAsync(Guid id);
    }
}