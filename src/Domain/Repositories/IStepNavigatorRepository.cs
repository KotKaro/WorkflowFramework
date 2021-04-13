using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IStepNavigatorRepository
    {
        Task CreateAsync(StepNavigator stepNavigator);
    }
}