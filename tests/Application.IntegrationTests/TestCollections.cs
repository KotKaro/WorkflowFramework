using Xunit;

namespace Application.IntegrationTests
{
    public class TestCollections
    {
        [CollectionDefinition(nameof(ApplicationIntegrationCollection))]
        public class ApplicationIntegrationCollection : ICollectionFixture<ApplicationFixture>
        {
            
        }
    }
}