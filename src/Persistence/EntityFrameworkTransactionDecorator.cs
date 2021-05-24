using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence
{
public class EntityFrameworkTransactionDecorator : IDbTransaction
{
    private readonly IDbContextTransaction _transaction;

    public IDbConnection Connection { get; }
    public IsolationLevel IsolationLevel { get; }
    
    public EntityFrameworkTransactionDecorator(IDbContextTransaction transaction)
    {
        _transaction = transaction;
        Connection = transaction.GetDbTransaction().Connection;
        IsolationLevel = transaction.GetDbTransaction().IsolationLevel;
    }
    
    public void Dispose()
    {
        _transaction.Dispose();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }
    }
}