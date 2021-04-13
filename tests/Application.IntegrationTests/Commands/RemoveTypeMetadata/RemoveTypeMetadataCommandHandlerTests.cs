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
    public class RemoveTypeMetadataCommandHandlerTests
    {
        private readonly ApplicationFixture _applicationFixture;

        public RemoveTypeMetadataCommandHandlerTests(ApplicationFixture applicationFixture)
        {
            _applicationFixture = applicationFixture;
            
            var context = _applicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;
            
            context!.Set<MemberDescriptor>().RemoveRange( context!.Set<MemberDescriptor>());
            context!.Set<ValueAccessor>().RemoveRange( context!.Set<ValueAccessor>());
            context!.Set<TypeMetadata>().RemoveRange( context!.Set<TypeMetadata>());
            context!.SaveChanges();
        }
        
        [Fact]
        public void When_IncorrectDataProvided_Expect_ValidationExceptionThrown()
        {
            //Arrange
            var mediator = _applicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;

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
            var mediator = _applicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context = _applicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as WorkflowFrameworkDbContext;

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