using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IProcessRunRepository
    {
        Task CreateAsync(ProcessRun processRun);
    }
}