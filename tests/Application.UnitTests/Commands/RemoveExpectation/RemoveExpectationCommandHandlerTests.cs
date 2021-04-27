using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.RemoveExpectation;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;
using Domain.ProcessAggregate.Expectations.CompareExpectations;
using Domain.Repositories;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.RemoveExpectation
{
    public class RemoveExpectationCommandHandlerTests
    {
        [Fact]
        public async Task When_ExpectationWithProvidedIdDoesNotExists_Expect_ObjectNotFoundExceptionThrown()
        {
            //Arrange
            var sut = CreateHandler(out _);
            
            //Act + Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(async () =>
            {
                await sut.Handle(new RemoveExpectationCommand
                {
                    ExpectationId = Guid.Empty
                }, CancellationToken.None);
            });
        }
        
        [Fact]
        public async Task When_ExpectationWithProvidedIdExits_Expect_ExpectationRepositoryRemoveCalled()
        {
            //Arrange
            var sut = CreateHandler(out var expectationRepositoryMock);

            expectationRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EqualExpectation(new ValueAccessor("test", GetType(), GetType()), "test"));

            //Act
            await sut.Handle(new RemoveExpectationCommand
            {
                ExpectationId = Guid.Empty
            }, CancellationToken.None);
            
            //Assert
            expectationRepositoryMock.Verify(x => x.Remove(It.IsAny<Expectation>()), Times.Once);

        }

        private static RemoveExpectationCommandHandler CreateHandler(out Mock<IExpectationRepository> expectationRepositoryMock)
        {
            expectationRepositoryMock = new Mock<IExpectationRepository>();
            var sut = new RemoveExpectationCommandHandler(expectationRepositoryMock.Object);
            return sut;
        }
    }
}