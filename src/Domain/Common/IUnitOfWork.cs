using System.Data;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IUnitOfWork
    {
        IDbTransaction BeginTransaction();
        Task<IDbTransaction> BeginTransactionAsync();
        Task SaveAsync();
    }
}