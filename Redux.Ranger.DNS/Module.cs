using System.Net;
using ARSoft.Tools.Net.Dns;
using Autofac;

namespace Redux.Ranger.DNS
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<StartHandler>().AsImplementedInterfaces();//.As<IRequestHandler<Start, MediatR.Unit>>().SingleInstance();
            builder.RegisterType<StopHandler>().AsImplementedInterfaces();//.As<IRequestHandler<Stop, MediatR.Unit>>().SingleInstance();
          
            var dnsAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            builder.Register(p => new DnsServer(dnsAddress, 10, 10, (query, address, type) => p.Resolve<StartHandler>().ProcessQuery(query, address, type)))
                .SingleInstance();


        }
    }
}
