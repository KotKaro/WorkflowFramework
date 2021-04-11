using System;

namespace Domain.Common
{
    public interface IRepositoryFactory
    {
        IRepository GetByEntityType(Type entityType);
    }
}