using Autofac.Integration.WebApi;
using Bootstrap;
using MediatR;
using Owin;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger
{
    public class StartupConfigHandler : RequestHandler<StartupConfig>
    {

        protected override void HandleCore(StartupConfig message)
        {
            var container = Bootstrapper.Container as Autofac.Core.Container;

            var appBuilder = message.AppBuilder;
            var config = message.Configuration;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
        }
    }
}
