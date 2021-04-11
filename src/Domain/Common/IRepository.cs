using System.Collections.Generic;

namespace Domain.Common
{
    public interface IRepository
    {
        IEnumerable<object> GetAll();
    }
}