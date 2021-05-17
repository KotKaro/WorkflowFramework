using System.Linq;
using System.Threading.Tasks;
using Application.IntegrationTests.Commands;
using Application.Queries.GetProcesses;
using Domain.ProcessAggregate;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Queries.GetProcesses
{
    [Collection(nameof(TestCollections.ApplicationIntegrationCollection))]
    public class GetProcessesQueryHandler : CommandTestBase
    {
        public GetProcessesQueryHandler(ApplicationFixture applicationFixture) : base(applicationFixture)
        {
        }

        [Fact]
        public async Task When_GetProcessesQuerySentWith100ExistingProcesses_Expect_HundredProcessesReturned()
        {
            //Arrange
            for (var i = 0; i < 100; i++)
            {
                await Context.Set<Process>().AddAsync(new Process($"test{i}"));
            }

            await Context.SaveChangesAsync();

            //Act
            var processes = (await Mediator.Send(new GetProcessesQuery())).ToArray();

            //Assert
            processes.Length.Should().Be(100);
            ;
        }

        [Fact]
        public async Task When_GetProcessesQuery_ReturnedProcessDtoGotNameAndId()
        {
            //Arrange
            await Context.Set<Process>().AddAsync(new Process($"test"));
            await Context.SaveChangesAsync();

            //Act
            var processes = (await Mediator.Send(new GetProcessesQuery())).ToArray();

            //Assert
            processes[0].Id.Should().NotBeEmpty();
            processes[0].Name.Should().NotBeEmpty();
        }
    }
}