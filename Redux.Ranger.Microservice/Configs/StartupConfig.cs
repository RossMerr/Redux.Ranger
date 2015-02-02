using System.Web.Http;
using MediatR;
using Owin;

namespace Redux.Ranger.Microservice.Configs
{
    public class StartupConfig
    {
        // add an extra parameter of type IKernel
        public void Configure(IAppBuilder appBuilder, IMediator mediator)
        {
            var config = new HttpConfiguration();
            
            config.MapHttpAttributeRoutes();
            config.MapDefinedRoutes();

            mediator.Send(new Notification.StartupConfig()
            {
                AppBuilder = appBuilder,
                Configuration = config
            });

            appBuilder.UseWebApi(config);
        }
    } 
}
