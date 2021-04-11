using System.Data;
using System.Threading.Tasks;
using Domain.Common;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkflowFrameworkDbContext _dbContext;

        public UnitOfWork(WorkflowFrameworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbTransaction BeginTransaction()
        {
            return new EntityFrameworkTransactionDecorator(_dbContext.Database.BeginTransaction());
        }

        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            return new EntityFrameworkTransactionDecorator(await _dbContext.Database.BeginTransactionAsync());
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}