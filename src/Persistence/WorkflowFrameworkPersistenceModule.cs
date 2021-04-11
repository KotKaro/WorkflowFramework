using Autofac;
using Domain.Common;
using Domain.Repositories;
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
            
            builder.Register(ctx => new WorkflowFrameworkDbContext(ctx.Resolve<DbContextOptions<WorkflowFrameworkDbContext>>()))
                .AsSelf()
                .InstancePerLifetimeScope();
            
            builder.Register(ctx => new WorkflowFrameworkDbContext(ctx.Resolve<DbContextOptions<WorkflowFrameworkDbContext>>()))
                .AsSelf()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TypeMetadataRepository>()
                .As<ITypeMetadataRepository>()
                .InstancePerLifetimeScope();
        }
    }
}