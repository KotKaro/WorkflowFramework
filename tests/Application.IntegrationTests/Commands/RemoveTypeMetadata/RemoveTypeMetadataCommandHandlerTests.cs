using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CreateTypeMetadata;
using Domain.ProcessAggregate;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.RemoveTypeMetadata
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class RemoveTypeMetadataCommandHandlerTests : CommandTestBase
    {
        public RemoveTypeMetadataCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
           
        }
        
        [Fact]
        public void When_IncorrectDataProvided_Expect_ValidationExceptionThrown()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

            //Act + Assert
            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await mediator!.Send(new Application.Commands.RemoveTypeMetadata.RemoveTypeMetadataCommand
                {
                    AssemblyFullName = "",
                    TypeFullName = ""
                });
            });
        }
        
        [Fact]
        public async Task When_CorrectDataProvided_Expect_TypeMetadataRemovedFromDatabase()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context = ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;

            await mediator!.Send(new CreateTypeMetadataCommand
            {
                AssemblyFullName = typeof(TestClass).Assembly.FullName,
                TypeFullName = typeof(TestClass).FullName
            });
            
            context!.Set<TypeMetadata>().Count().Should().Be(1);
            
            //Act
            await mediator!.Send(new Application.Commands.RemoveTypeMetadata.RemoveTypeMetadataCommand
            {
                AssemblyFullName = typeof(TestClass).Assembly.FullName,
                TypeFullName = typeof(TestClass).FullName
            });
            
            //Assert
            context!.Set<TypeMetadata>().Count().Should().Be(0);
        }
    }
}