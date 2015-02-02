using System.Reflection;
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
