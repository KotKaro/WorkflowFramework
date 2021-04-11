using Domain.Common;
using Domain.Services;
using Moq;

namespace Domain.UnitTests.DataFactories
{
    public static class SpecificationResolverServiceFactory
    {
        public static ExpectationResolverService Create(IRepositoryFactory repositoryFactory = null!)
        {
            repositoryFactory ??= new Mock<IRepositoryFactory>().Object;
            return new ExpectationResolverService(repositoryFactory);
        }
    }
}