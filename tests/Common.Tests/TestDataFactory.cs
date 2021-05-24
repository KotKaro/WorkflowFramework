using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.ValueObjects;
using Domain.ProcessAggregate;
using Domain.Repositories;
using Moq;

namespace Common.Tests
{
    public class TestDataFactory
    {
        public static Process CreateProcess(string name = "some process", IEnumerable<Step> steps = null)
        {
            var processRepository = new Mock<IProcessRepository>();

            processRepository.Setup(x => x.GetByIdAsync(name))
                .Returns(Task.FromResult(null as Process));

            var stepsList = steps?.ToList();
            return Process.Create(
                new Name(name),
                stepsList, 
                processRepository.Object
            );
        }
        
        public static Step CreateStep(string stepName = "stepName")
        {
            return new(CreateStepName(stepName));
        }

        public static Name CreateStepName(string name = "step name")
        {
            return new Name(name);
        }
    }
}