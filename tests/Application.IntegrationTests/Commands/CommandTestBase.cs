using Domain.ProcessAggregate;
using Domain.ProcessAggregate.Expectations;
using MediatR;
using Persistence;

namespace Application.IntegrationTests.Commands
{
    public class CommandTestBase
    {
        protected readonly ApplicationFixture ApplicationFixture;
        protected readonly WorkflowFrameworkDbContext Context;
        protected readonly IMediator Mediator;

        protected CommandTestBase(ApplicationFixture applicationFixture)
        {
            ApplicationFixture = applicationFixture;

            Context =
                ApplicationFixture.Host.Services.GetService(typeof(WorkflowFrameworkDbContext)) as
                    WorkflowFrameworkDbContext;
            
            Mediator =
                ApplicationFixture.Host.Services.GetService(typeof(IMediator)) as
                    IMediator;
           
            Context!.Set<Argument>().RemoveRange(Context!.Set<Argument>());
            Context!.Set<ValueProvider>().RemoveRange(Context!.Set<ValueProvider>());
            Context!.Set<MemberDescriptor>().RemoveRange(Context!.Set<MemberDescriptor>());
            Context!.Set<Expectation>().RemoveRange(Context!.Set<Expectation>());
            Context!.Set<TypeMetadata>().RemoveRange(Context!.Set<TypeMetadata>());
            Context!.Set<StepNavigator>().RemoveRange(Context!.Set<StepNavigator>());
            Context!.Set<Step>().RemoveRange(Context!.Set<Step>());
            Context!.Set<Process>().RemoveRange(Context!.Set<Process>());
            Context!.Set<ProcessRun>().RemoveRange(Context!.Set<ProcessRun>());
            Context!.SaveChanges();
        }
    }
}