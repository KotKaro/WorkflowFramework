using System;
using Application.Commands.AddExpectation.AggregateExpectation;
using Application.UnitTests.Commands.AddExpectation.CompareExpectation.AddEqualExpectation;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Commands.AddExpectation.AggregateExpectation
{
    public abstract class AddAggregateExpectationCommandHandlerTestsBase<THandler, TCommand>
        where THandler : AddAggregateExpectationCommandHandler<TCommand>
        where TCommand : AddAggregateExpectationCommand
        
    {
        protected THandler CreateHandler(
            out Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock,
            out Mock<IExpectationRepository> expectationRepository,
            out StepNavigator stepNavigator
        )
        {
            var valueProvider = new ValueProvider(
                "test",
                typeof(AddEqualExpectationCommandHandlerTests),
                typeof(AddEqualExpectationCommandHandlerTests)
            );
            stepNavigator = new StepNavigator(new Step("test"));
            
            stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();
            expectationRepository = new Mock<IExpectationRepository>();

            stepNavigatorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(stepNavigator);

            expectationRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EqualExpectation(valueProvider, "test"));

            return CreateHandler(stepNavigatorRepositoryMock, expectationRepository);
        }

        protected abstract THandler CreateHandler(
            Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock,
            Mock<IExpectationRepository> expectationRepository
        );
    }
}