using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using Autofac;
using Autofac.Integration.WebApi;
using Redux.Ranger.Client;
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

            builder.Register(p => new MicroserviceConfiguration(8000))
                .As<MicroserviceConfiguration>().PreserveExistingDefaults();

            builder.RegisterType<BaseService>();

        }

        private static void LoadFromAllAssemblies(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IExternalComponentChecker>())
                .AsImplementedInterfaces().SingleInstance();

            //builder.RegisterAssemblyTypes(assembly).Where(p => p.IsAssignableTo<ApiController>())
            //    .InstancePerLifetimeScope().As<ApiController>();

            builder.RegisterApiControllers(assembly);

        }
    }
}
