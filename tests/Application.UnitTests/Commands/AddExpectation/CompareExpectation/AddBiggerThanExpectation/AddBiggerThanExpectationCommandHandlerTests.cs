using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddExpectation.CompareExpectation.AddBiggerThanExpectation;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.AddExpectation.CompareExpectation.AddBiggerThanExpectation
{
    public class AddBiggerThanExpectationCommandHandlerTests : AddCompareExpectationCommandHandlerTestsBase<AddBiggerThanExpectationCommandHandler, AddBiggerThanExpectationCommand>
    {
        [Fact]
        public async Task When_CommandIsCorrect_Expect_AddedExpectationIsEqualExpectation()
        {
            //Arrange
            var sut = CreateHandler(
                out _,
                out _,
                out _,
                out var stepNavigator
            );

            //Act
            await sut.Handle(new AddBiggerThanExpectationCommand
            {
                ValueProviderId = Guid.NewGuid(),
                StepNavigatorId = Guid.NewGuid(),
                Value = "test"
            }, CancellationToken.None);

            //Assert
            stepNavigator.Expectations.First().GetType().Should().Be(typeof(BiggerThanExpectation));
        }

        protected override AddBiggerThanExpectationCommandHandler CreateHandler(Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock, Mock<IValueProviderRepository> valueProviderRepositoryMock,
            Mock<IExpectationRepository> expectationRepository)
        {
            return new(
                stepNavigatorRepositoryMock.Object,
                valueProviderRepositoryMock.Object,
                expectationRepository.Object
            );
        }
    }
}