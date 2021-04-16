using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddExpectation.CompareExpectation.AddLessThanExpectation;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.AddExpectation.CompareExpectation.AddLessThanExpectation
{
    public class AddLessThanExpectationCommandHandlerTests : AddCompareExpectationCommandHandlerTestsBase<AddLessThanExpectationCommandHandler, AddLessThanExpectationCommand>
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
            await sut.Handle(new AddLessThanExpectationCommand
            {
                ValueAccessorId = Guid.NewGuid(),
                StepNavigatorId = Guid.NewGuid(),
                Value = "test"
            }, CancellationToken.None);

            //Assert
            stepNavigator.Expectations.First().GetType().Should().Be(typeof(LessThanExpectation));
        }

        protected override AddLessThanExpectationCommandHandler CreateHandler(Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock, Mock<IValueAccessorRepository> valueAccessorRepositoryMock,
            Mock<IExpectationRepository> expectationRepository)
        {
            return new(
                stepNavigatorRepositoryMock.Object,
                valueAccessorRepositoryMock.Object,
                expectationRepository.Object
            );
        }
    }
}