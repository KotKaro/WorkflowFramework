using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.CreateProcessRun;
using Domain.ProcessAggregate;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Persistence;
using Xunit;

namespace Application.IntegrationTests.Commands.CreateProcessRun
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class CreateProcessRunCommandHandlerTests : CommandTestBase
    {
        public CreateProcessRunCommandHandlerTests(ApplicationFixture applicationFixture) : base(applicationFixture)
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
                await mediator!.Send(new CreateProcessRunCommand
                {
                    ProcessId = Guid.Empty,
                    StartStepId = Guid.Empty
                });
            });
        }

        [Fact]
        public async Task When_CorrectDataProvidedWithoutArguments_Expect_TypeMetadataAddedToDatabase()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context =
                ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as
                    WorkflowFrameworkDbContext;

            var typeMetadata = new TypeMetadata(typeof(TestClass));
            
            var process = new Process("test");
            var step = new Step("step");
            process.AddStep(step);

            await context!.Set<Step>().AddAsync(step);
            await context.Set<Process>().AddAsync(process);
            await context.Set<TypeMetadata>().AddAsync(typeMetadata);
            await context.SaveChangesAsync();
            
            //Act
            await mediator!.Send(new CreateProcessRunCommand
            {
                ProcessId = process.Id,
                StartStepId = step.Id,
            });

            //Assert
            context!.Set<ProcessRun>().Count().Should().Be(1);
        }
        
        [Fact]
        public async Task When_CorrectDataProvidedWithArguments_Expect_AddedProcessRunGotArguments()
        {
            //Arrange
            var mediator = ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as IMediator;
            var context =
                ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as
                    WorkflowFrameworkDbContext;

            var typeMetadata = new TypeMetadata(typeof(TestClass));
            
            var process = new Process("test");
            var step = new Step("step");
            process.AddStep(step);

            await context!.Set<Step>().AddAsync(step);
            await context.Set<Process>().AddAsync(process);
            await context.Set<TypeMetadata>().AddAsync(typeMetadata);
            await context.SaveChangesAsync();
            
            //Act
            await mediator!.Send(new CreateProcessRunCommand
            {
                ProcessId = process.Id,
                StartStepId = step.Id,
                ArgumentDTOs = new []
                {
                    new ArgumentDto
                    {
                        Value = "test",
                        MemberDescriptorId = typeMetadata.ValueAccessors.First().Id
                    }
                }
            });

            //Assert
            var processRun = context!.Set<ProcessRun>().First();
            processRun.Arguments.Count().Should().Be(1);
        }
    }
}