using System;
using Bootstrap;
using Bootstrap.Autofac;
using Bootstrap.Locator;
using Common.Logging;

namespace Redux.Ranger.Microservice
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger<Program>();

        public static void Main()
        {
            Log.Info("Bootstrapping ");

            Bootstrapper.With.Autofac().With.ServiceLocator().With.Start();

            var service = new OwinServiceHost(new Uri("http://localhost"), new BaseService())
            {
                ServiceName = "Microservice",
                ServiceDisplayName = "Microservice",
                ServiceDescription = "Microservice"
            };
            service.Initialize();

            Log.Info("Bootstrapped");

        }
    }
}