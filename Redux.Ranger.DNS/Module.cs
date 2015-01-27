using System.Net;
using Autofac;
using Redux.Ranger.Microservice;

namespace Redux.Ranger.DNS
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(p => new MicroserviceConfiguration(IPAddress.Parse("8.8.8.8"), 8000))
                .As<MicroserviceConfiguration>().AsSelf();

            builder.RegisterType<BaseService>().As<BaseService>();

            builder.RegisterType<DnsService>().As<IService>();

        }
    }
}
