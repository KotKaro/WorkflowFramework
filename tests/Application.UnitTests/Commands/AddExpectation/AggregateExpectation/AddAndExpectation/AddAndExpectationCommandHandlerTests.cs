using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddExpectation.AggregateExpectation.AddAndExpectation;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.AddExpectation.AggregateExpectation.AddAndExpectation
{
    public class AddAndExpectationCommandHandlerTests : AddAggregateExpectationCommandHandlerTestsBase<AddAndExpectationCommandHandler, AddAndExpectationCommand>
    {
        [Fact]
        public async Task When_StepNavigatorDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var handler = CreateHandler(
                out var stepNavigatorRepositoryMock,
                out _,
                out _
            );
            
            stepNavigatorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as StepNavigator);

            //Act + Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await handler.Handle(new AddAndExpectationCommand
                {
                    ExpectationIds = Enumerable.Range(0, 100).Select(_ => Guid.NewGuid()),
                    StepNavigatorId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_AnyOfProvidedExpectationsDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var expectationIds = Enumerable.Range(0, 100).Select(_ => Guid.NewGuid());
            
            var handler = CreateHandler(
                out _,
                out var expectationRepository,
                out _
            );
            
            expectationRepository.Setup(x => x.GetByIdAsync(expectationIds.Last()))
                .ReturnsAsync(null as Expectation);

            //Act + Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await handler.Handle(new AddAndExpectationCommand
                {
                    ExpectationIds = expectationIds,
                    StepNavigatorId = Guid.NewGuid()
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_StepNavigatorExistsAndAllExpectationsExists_Expect_ExpectationRepositoryCreateAsyncCalled()
        {
            //Arrange
            var expectations = Enumerable.Range(0, 100)
                .Select(_ => new EqualExpectation(new ValueAccessor("test", GetType()), "test"))
                .ToArray();

            var handler = CreateHandler(
                out _,
                out var expectationRepository,
                out _
            );

            expectationRepository.Setup(x => x.GetByIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(expectations);
            
            //Act
            await handler.Handle(new AddAndExpectationCommand
            {
                ExpectationIds = expectations.Select(x => x.Id),
                StepNavigatorId = Guid.NewGuid()
            }, CancellationToken.None);
            
            //Assert
            expectationRepository.Verify(x => x.CreateAsync(It.IsAny<Expectation>()), Times.Once);
        }
        
        [Fact]
        public async Task When_StepNavigatorExistsAndAllExpectationsExists_Expect_StepNavigatorGotExpectationAdded()
        {
            var expectations = Enumerable.Range(0, 100)
                .Select(_ => new EqualExpectation(new ValueAccessor("test", GetType()), "test"))
                .ToArray();

            var handler = CreateHandler(
                out _,
                out var expectationRepository,
                out var stepNavigator
            );
            
            expectationRepository.Setup(x => x.GetByIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(expectations);
            
            //Act
            await handler.Handle(new AddAndExpectationCommand
            {
                ExpectationIds = expectations.Select(x => x.Id),
                StepNavigatorId = Guid.NewGuid()
            }, CancellationToken.None);
            
            //Assert
            stepNavigator.Expectations.Count().Should().Be(1);
        }
        
        [Fact]
        public async Task When_StepNavigatorExistsAndAllExpectationsExists_Expect_AddedExpectationIsAndExpectation()
        {
            var expectations = Enumerable.Range(0, 100)
                .Select(_ => new EqualExpectation(new ValueAccessor("test", GetType()), "test"))
                .ToArray();

            var handler = CreateHandler(
                out _,
                out var expectationRepository,
                out var stepNavigator
            );
            
            expectationRepository.Setup(x => x.GetByIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(expectations);
            
            //Act
            await handler.Handle(new AddAndExpectationCommand
            {
                ExpectationIds = expectations.Select(x => x.Id),
                StepNavigatorId = Guid.NewGuid()
            }, CancellationToken.None);
            
            //Assert
            stepNavigator.Expectations.First().GetType().Should().Be(typeof(AndExpectation));
        }
        
        protected override AddAndExpectationCommandHandler CreateHandler(
            Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock,
            Mock<IExpectationRepository> expectationRepository)
        {
            return new(expectationRepository.Object, stepNavigatorRepositoryMock.Object);
        }
    }
}