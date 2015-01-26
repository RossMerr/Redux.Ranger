using System.Web.Http;

namespace Redux.Ranger.Microservice.Configs
{
    public static class RoutesConfig
    {
        public static void MapDefinedRoutes(this HttpConfiguration config)
        {
            config.Routes.MapHttpRoute
            (
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );
        }
    }
}
