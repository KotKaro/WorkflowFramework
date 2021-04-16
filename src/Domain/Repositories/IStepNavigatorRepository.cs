using System;
using System.Threading.Tasks;
using Domain.ProcessAggregate;

namespace Domain.Repositories
{
    public interface IStepNavigatorRepository
    {
        void Update(StepNavigator stepNavigator);
        Task CreateAsync(StepNavigator stepNavigator);
        void Remove(StepNavigator stepNavigator);
        Task<StepNavigator> GetByIdAsync(Guid id);
    }
}