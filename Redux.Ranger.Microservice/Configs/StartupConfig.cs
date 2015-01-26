using System.Web.Http;
using Autofac.Integration.WebApi;
using Bootstrap;
using Owin;

namespace Redux.Ranger.Microservice.Configs
{
    public class StartupConfig
    {
        // add an extra parameter of type IKernel
        public void Configure(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.MapDefinedRoutes();

            var container = Bootstrapper.Container as Autofac.Core.Container;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);


            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }
    } 
}
