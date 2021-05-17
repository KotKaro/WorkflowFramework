using System;
using Application.Commands.AddExpectation.CompareExpectation;
using Application.UnitTests.Commands.AddExpectation.CompareExpectation.AddEqualExpectation;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Commands.AddExpectation.CompareExpectation
{
    public abstract class AddCompareExpectationCommandHandlerTestsBase<THandler, TCommand>
        where THandler : AddCompareExpectationCommandHandlerBase<TCommand>
        where TCommand : AddCompareExpectationCommand
        
    {
        protected THandler CreateHandler(
            out Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock,
            out Mock<IValueProviderRepository> valueProviderRepositoryMock,
            out Mock<IExpectationRepository> expectationRepository,
            out StepNavigator stepNavigator
        )
        {
            var ValueProvider = new ValueProvider(
                "test",
                typeof(AddEqualExpectationCommandHandlerTests),
                typeof(AddEqualExpectationCommandHandlerTests)
            );
            stepNavigator = new StepNavigator(new Step("test"));
            
            stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();
            valueProviderRepositoryMock = new Mock<IValueProviderRepository>();
            expectationRepository = new Mock<IExpectationRepository>();

            stepNavigatorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(stepNavigator);

            valueProviderRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(ValueProvider);

            expectationRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EqualExpectation(ValueProvider, "test"));

            return CreateHandler(stepNavigatorRepositoryMock, valueProviderRepositoryMock, expectationRepository);
        }

        protected abstract THandler CreateHandler(
            Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock,
            Mock<IValueProviderRepository> valueProviderRepositoryMock,
            Mock<IExpectationRepository> expectationRepository
        );
    }
}