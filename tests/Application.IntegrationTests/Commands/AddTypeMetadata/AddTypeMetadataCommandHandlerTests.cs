using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CreateTypeMetadata;
using Domain.ProcessAggregate;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.AddTypeMetadata
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class AddTypeMetadataCommandHandlerTests : CommandTestBase
    {
        public AddTypeMetadataCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
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
                await mediator!.Send(new CreateTypeMetadataCommand
                {
                    AssemblyFullName = "",
                    TypeFullName = ""
                });
            });
        }
        
        [Fact]
        public async Task When_CorrectDataProvided_Expect_TypeMetadataAddedToDatabase()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

            //Act
            await mediator!.Send(new CreateTypeMetadataCommand
            {
                AssemblyFullName = typeof(TestClass).Assembly.FullName,
                TypeFullName = typeof(TestClass).FullName
            });
            
            //Assert
            var context = ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;
            context!.Set<TypeMetadata>().Count().Should().Be(1);
        }
    }
}