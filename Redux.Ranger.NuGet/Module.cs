using Autofac;
using Redux.Ranger.Microservice;

namespace Redux.Ranger.NuGet
{
    public class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyGetService>().As<IService>();

            NuGetRoutes.Start();
        }
    }
}
