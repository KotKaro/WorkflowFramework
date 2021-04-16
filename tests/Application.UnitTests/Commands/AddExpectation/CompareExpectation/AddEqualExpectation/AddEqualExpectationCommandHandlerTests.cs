using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddExpectation.CompareExpectation.AddEqualExpectation;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.AddExpectation.CompareExpectation.AddEqualExpectation
{
    public class AddEqualExpectationCommandHandlerTests 
        : AddCompareExpectationCommandHandlerTestsBase<AddEqualExpectationCommandHandler, AddEqualExpectationCommand>
    {
        [Fact]
        public void When_StepNavigatorNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var sut = CreateHandler(
                out var stepNavigatorRepositoryMock,
                out _,
                out _,
                out _
            );

            stepNavigatorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as StepNavigator);

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new AddEqualExpectationCommand
                {
                    ValueAccessorId = Guid.NewGuid(),
                    StepNavigatorId = Guid.NewGuid(),
                    Value = "test"
                }, CancellationToken.None);
            });
        }

        [Fact]
        public void When_ValueAccessorNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var sut = CreateHandler(
                out _,
                out var valueAccessorRepositoryMock,
                out _,
                out _
            );

            //Act
            valueAccessorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as ValueAccessor);

            //Act + Assert
            Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new AddEqualExpectationCommand
                {
                    ValueAccessorId = Guid.NewGuid(),
                    StepNavigatorId = Guid.NewGuid(),
                    Value = "test"
                }, CancellationToken.None);
            });

            //Assert
        }

        [Fact]
        public async Task When_CommandIsCorrect_Expect_ExpectationRepositoryCreateAsyncCalledOnce()
        {
            //Arrange
            var sut = CreateHandler(
                out _,
                out _,
                out var expectationRepository,
                out _
            );

            //Act
            await sut.Handle(new AddEqualExpectationCommand
            {
                ValueAccessorId = Guid.NewGuid(),
                StepNavigatorId = Guid.NewGuid(),
                Value = "test"
            }, CancellationToken.None);

            //Assert
            expectationRepository.Verify(x => x.CreateAsync(It.IsAny<EqualExpectation>()), Times.Once);
        }
        
        [Fact]
        public async Task When_CommandIsCorrect_Expect_StepNavigatorGotOneExpectation()
        {
            //Arrange
            var sut = CreateHandler(
                out _,
                out _,
                out _,
                out var stepNavigator
            );

            //Act
            await sut.Handle(new AddEqualExpectationCommand
            {
                ValueAccessorId = Guid.NewGuid(),
                StepNavigatorId = Guid.NewGuid(),
                Value = "test"
            }, CancellationToken.None);

            //Assert
            stepNavigator.Expectations.Count().Should().Be(1);
        }
        
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
            await sut.Handle(new AddEqualExpectationCommand
            {
                ValueAccessorId = Guid.NewGuid(),
                StepNavigatorId = Guid.NewGuid(),
                Value = "test"
            }, CancellationToken.None);

            //Assert
            stepNavigator.Expectations.First().GetType().Should().Be(typeof(EqualExpectation));
        }

        protected override AddEqualExpectationCommandHandler CreateHandler(Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock, Mock<IValueAccessorRepository> valueAccessorRepositoryMock,
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