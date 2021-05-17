using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AddExpectation.AggregateExpectation.AddOrExpectation;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations.AggregateExpectations;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.AddExpectation.AggregateExpectation.AddOrExpectation
{
    public class AddOrExpectationCommandHandlerTests : AddAggregateExpectationCommandHandlerTestsBase<AddOrExpectationCommandHandler, AddOrExpectationCommand>
    {
        [Fact]
        public async Task When_StepNavigatorExistsAndAllExpectationsExists_Expect_AddedExpectationIsOrExpectation()
        {
            var expectations = Enumerable.Range(0, 100)
                .Select(_ => new EqualExpectation(new ValueProvider("test", GetType(), GetType()), "test"))
                .ToArray();

            var handler = CreateHandler(
                out _,
                out var expectationRepository,
                out var stepNavigator
            );
            
            expectationRepository.Setup(x => x.GetByIds(It.IsAny<IEnumerable<Guid>>()))
                .Returns(expectations);
            
            //Act
            await handler.Handle(new AddOrExpectationCommand
            {
                ExpectationIds = expectations.Select(x => x.Id),
                StepNavigatorId = Guid.NewGuid()
            }, CancellationToken.None);
            
            //Assert
            stepNavigator.Expectations.First().GetType().Should().Be(typeof(OrExpectation));
        }
        
        protected override AddOrExpectationCommandHandler CreateHandler(
            Mock<IStepNavigatorRepository> stepNavigatorRepositoryMock,
            Mock<IExpectationRepository> expectationRepository)
        {
            return new(expectationRepository.Object, stepNavigatorRepositoryMock.Object);
        }
    }
}