using Autofac;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence
{
    public class WorkflowFrameworkPersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(context => context.Resolve<DbContextOptions<WorkflowFrameworkDbContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<WorkflowFrameworkDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo(typeof(RepositoryBase))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}