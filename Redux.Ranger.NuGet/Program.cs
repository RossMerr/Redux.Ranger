using System;
using Bootstrap;
using Bootstrap.Autofac;
using Microsoft.Practices.ServiceLocation;
using Redux.Ranger.Microservice;

namespace Redux.Ranger.NuGet
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.With.Autofac().With.Start();

            var microService = ServiceLocator.Current.GetInstance<IService>();

            var service = new OwinServiceHost(new Uri("http://localhost/"), microService)
            {
                ServiceName = "NuGet",
                ServiceDisplayName = "NuGet Service",
                ServiceDescription = "NuGet service"
            };
            service.Initialize();
        }
    }
}
