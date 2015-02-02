using Autofac;
using MediatR;
using Redux.Ranger.Microservice;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger.NuGet
{
    public class Module: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartHandler>().As<IRequestHandler<Start, MediatR.Unit>>();
            builder.RegisterType<StopHandler>().As<IRequestHandler<Stop, MediatR.Unit>>();


            NuGetRoutes.Start();
        }
    }
}
