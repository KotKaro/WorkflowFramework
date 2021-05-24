using System;
using System.Linq;
using Common.Tests;
using Domain.Common;
using Domain.ProcessAggregate;
using Domain.Services;
using Domain.UnitTests.DataFactories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class StepNavigatorTests
    {
        private readonly ExpectationResolverService _expectationResolverService;

        public StepNavigatorTests()
        {
            _expectationResolverService = SpecificationResolverServiceFactory.Create();
        }
        
        [Fact]
        public void When_StepNavigatorCreatedWithoutTargetStep_Expect_ArgumentNullExceptionThrown()
        {
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new StepNavigator(null);
            });
        }

        [Fact]
        public void When_CanMoveCheckForNavigatorWithoutAnyExpectation_Expect_TrueReturned()
        {
            //Arrange
            var sut = new StepNavigator(TestDataFactory.CreateStep());

            //Act
            var result = sut.CanMove(_expectationResolverService);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void When_CanMoveCalledForNavigatorWithExpectation_Expect_PredicateIsMatchedMethodCalledForEachObjectFromRepository()
        {
            //Arrange
            var expectation = new FalseExpectation(typeof(TestClass));
            var sut = new StepNavigator(TestDataFactory.CreateStep());
            sut.AddExpectations(expectation);

            var repositoryMock = new Mock<IRepository>();
            var factoryMock = new Mock<IRepositoryFactory>();

            var getAllResult = Enumerable.Range(0, 100)
                .Select(x => (object) x)
                .ToArray();
            
            repositoryMock.Setup(x => x.GetAll())
                .Returns(getAllResult);
            
            factoryMock.Setup(x => x.GetByEntityType(It.IsAny<Type>()))
                .Returns(repositoryMock.Object);
                
            var specificationResolverService = SpecificationResolverServiceFactory.Create(factoryMock.Object);
            
            //Act
            sut.CanMove(specificationResolverService);

            //Assert
            expectation.InvokeCounter.Should().Be(getAllResult.Length);
        }
    }
}
