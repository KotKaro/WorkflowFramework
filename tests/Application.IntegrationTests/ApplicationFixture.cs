using System;
using System.Collections.Generic;
using System.Linq;
using API;
using Autofac;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Persistence;

namespace Application.IntegrationTests
{
    public class ApplicationFixture : IDisposable
    {
        public IHost Host { get; }
            
        public ApplicationFixture()
        {
            Host = Program.CreateHostBuilder(Array.Empty<string>())
                .Build();

            EnsureMigrationsApplied();
        }

        public void Dispose()
        {
            Host?.Dispose();
        }
            
        private void EnsureMigrationsApplied()
        {

            var dbContext = Host.Services.GetService<WorkflowFrameworkDbContext>();
            var appliedMigrationsCount = dbContext.Database.GetAppliedMigrations().Count();
            var availableMigrationsCount = dbContext.Database.GetMigrations().Count();

            if (availableMigrationsCount > appliedMigrationsCount)
            {
                dbContext.Database.Migrate();
            }
        }

        private IContainer BuildContainer(string connectionString)
        {
            var builder = new ContainerBuilder();

            builder.Register(_ => new SqlConnection(connectionString))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(_ => Mock.Of<IServiceProvider>())
                .As<IServiceProvider>()
                .InstancePerLifetimeScope();

            builder.Register(componentContext =>
                {
                    var serviceProvider = componentContext.Resolve<IServiceProvider>();
                    var dbContextOptions =
                        new DbContextOptions<WorkflowFrameworkDbContext>(new Dictionary<Type, IDbContextOptionsExtension>());

                    var optionsBuilder = new DbContextOptionsBuilder<WorkflowFrameworkDbContext>(dbContextOptions)
                        .UseApplicationServiceProvider(serviceProvider)
                        .UseSqlServer(componentContext.Resolve<SqlConnection>(),
                            serverOptions =>
                            {
                                serverOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                                serverOptions.MigrationsAssembly(GetType().Assembly.FullName);
                            }
                        );

                    return optionsBuilder.Options;
                })
                .As<DbContextOptions<WorkflowFrameworkDbContext>>()
                .InstancePerLifetimeScope();

            builder.RegisterModule<WorkflowFrameworkPersistenceModule>();
            builder.RegisterModule<WorkflowFrameworkApplicationModule>();

            return builder.Build();
        }
    }
}