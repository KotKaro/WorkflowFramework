using System.Collections.Generic;

namespace WorkflowFramework.Abstractions
{
    public interface IRepository<out T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}