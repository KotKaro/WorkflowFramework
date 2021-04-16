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
            out Mock<IValueAccessorRepository> valueAccessorRepositoryMock,
            out Mock<IExpectationRepository> expectationRepository,
            out StepNavigator stepNavigator
        )
        {
            var valueAccessor = new ValueAccessor("test", typeof(AddEqualExpectationCommandHandlerTests));
            stepNavigator = new StepNavigator(new Step("test"));
            
            stepNavigatorRepositoryMock = new Mock<IStepNavigatorRepository>();
            valueAccessorRepositoryMock = new Mock<IValueAccessorRepository>();
            expectationRepository = new Mock<IExpectationRepository>();

            stepNavigatorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(stepNavigator);

            valueAccessorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(valueAccessor);

            expectationRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EqualExpectation(valueAccessor, "test"));

            return CreateHandler(stepNavigatorRepositoryMock, valueAccessorRepositoryMock, expectationRepository);
        }

        protected abstract THandler CreateHandler(
            Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock,
            Mock<IValueAccessorRepository> valueAccessorRepositoryMock,
            Mock<IExpectationRepository> expectationRepository
        );
    }
}