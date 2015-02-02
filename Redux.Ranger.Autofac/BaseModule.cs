using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using MediatR;
using Microsoft.Practices.ServiceLocation;
using Redux.Ranger.Microservice.HealthMonitoring;
using Redux.Ranger.Microservice.Notification;
using Redux.Ranger.Microservice.Register;

namespace Redux.Ranger
{
    public class BaseModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var userService = Assembly.GetCallingAssembly();

            LoadFromAllAssemblies(builder, userService);

            var ranger = Assembly.GetExecutingAssembly();
            
            LoadFromAllAssemblies(builder, ranger);

            builder.RegisterInstance<ServiceLocatorProvider>(() => ServiceLocator.Current)
                .As<ServiceLocatorProvider>();
            
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(Start).Assembly).AsImplementedInterfaces();
            builder.RegisterInstance(Console.Out).As<TextWriter>();

            builder.RegisterType<RegisterHandler>().AsImplementedInterfaces();//.As<IRequestHandler<Start, MediatR.Unit>>();
            builder.RegisterType<UnregisterHandler>().AsImplementedInterfaces();//.As<IRequestHandler<Stop, MediatR.Unit>>();

        }

        private static void LoadFromAllAssemblies(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IExternalComponentChecker>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterApiControllers(assembly);
        }
    }
}
