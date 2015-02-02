using System;
using Bootstrap;
using Bootstrap.Autofac;
using Bootstrap.Locator;
using Microsoft.Practices.ServiceLocation;
using Redux.Ranger.Microservice;

namespace Redux.Ranger.DNS
{
    public class Program 
    {
        public static void Main()
        {
            Bootstrapper.With.Autofac().With.ServiceLocator().With.Start();

            var microService = ServiceLocator.Current.GetInstance<IService>();

            var service = new OwinServiceHost(new Uri("http://localhost:1002/"), microService)
            {
                ServiceName = "DNS",
                ServiceDisplayName = "DNS Service",
                ServiceDescription = "DNS service"
            };
            service.Initialize();
        }    
    }
}