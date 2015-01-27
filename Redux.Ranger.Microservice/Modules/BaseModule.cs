using System.Net;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Redux.Ranger.Microservice.HealthMonitoring;

namespace Redux.Ranger.Microservice.Modules
{
    public class BaseModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var userService = Assembly.GetCallingAssembly();

            LoadFromAllAssemblies(builder, userService);

            var ranger = Assembly.GetExecutingAssembly();
            
            LoadFromAllAssemblies(builder, ranger);

            builder.Register(p => new MicroserviceConfiguration(IPAddress.Parse("8.8.8.8"), 8000))
                .As<MicroserviceConfiguration>()
                .PreserveExistingDefaults();

            builder.RegisterType<Service>()
                .As<Service>()
                .SingleInstance();

            builder.RegisterType<BaseService>()
                .As<IService>()
                .PreserveExistingDefaults();

            builder.RegisterType<RegisterService>()
                .As<RegisterService>()
                .PreserveExistingDefaults();
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
