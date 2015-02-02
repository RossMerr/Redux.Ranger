using System.Net;
using Autofac;
using Redux.Ranger.Microservice;

namespace Redux.Ranger.DNS
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DnsService>().As<IService>();

        }
    }
}
