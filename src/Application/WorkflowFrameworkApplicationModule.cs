using System.Reflection;
using Application.PipelineBehaviours;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Module = Autofac.Module;

namespace Application
{
    public class WorkflowFrameworkApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterMediatR(ThisAssembly);
            
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsClosedTypeOf(typeof(IRequestHandler<>)))
                .As(typeof(IRequestHandler<>));
            
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => x.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .As(typeof(IRequestHandler<,>));
            
            builder.RegisterGeneric(typeof(TransactionBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));
            
            builder.RegisterGeneric(typeof(ValidationBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));
            
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAutoMapper(ThisAssembly);
        }

        protected override Assembly ThisAssembly => GetType().Assembly;
    }
}