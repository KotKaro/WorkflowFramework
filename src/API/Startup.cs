using System;
using System.Collections.Generic;
using Application;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
 
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            ApplyWorkflowDbContextMigrations(app);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = Configuration.GetConnectionString("database");

            builder.Register(_ => new SqlConnection(connectionString))
                .AsSelf()
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
        }
        
        private static void ApplyWorkflowDbContextMigrations(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<WorkflowFrameworkDbContext>();
            context!.Database.Migrate();
        }
    }
}