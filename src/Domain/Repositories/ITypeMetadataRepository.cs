using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface ITypeMetadataRepository
    {
        Task CreateAsync(TypeMetadata typeMetadata);
        void Remove(TypeMetadataDto typeMetadataDto);
    }
}