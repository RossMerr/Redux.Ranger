using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Bootstrap;
using Bootstrap.Autofac;
using Common.Logging;
using Redux.Ranger.Client;
using Redux.Ranger.Microservice.HealthMonitoring;
using Topshelf;
using Topshelf.Autofac;
using Topshelf.Common.Logging;

namespace Redux.Ranger.Microservice
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger<Program>();

        static void Main(string[] args)
        {
            Log.Info("Bootstrapping ");

            Bootstrapper.With.Autofac().Start();

            Log.Info("Bootstrapped");
            
            var container = Bootstrapper.Container as Autofac.Core.Container;

            var config = container.Resolve<MicroserviceConfiguration>();

            var name = System.Reflection.Assembly.GetEntryAssembly().GetName();

            HostFactory.Run(c =>
            {
                // Pass it to Topshelf
                c.UseAutofacContainer(container);
                //c.UseCommonLogging();
                c.Service<BaseService>(s =>
                {
                    // Let Topshelf use it
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted
                        (
                            (service, hostControl) =>
                                service.Start(hostControl)
                        );

                    s.WhenStopped
                        (
                            (service, hostControl) =>
                                service.Stop(hostControl)
                        );
                });

                c.SetServiceName(config.Name ?? name.Name);
                c.SetDisplayName(config.Name ?? name.Name);
                c.SetDescription(config.Description + " v:" + name.Version.ToString());
                
                c.EnablePauseAndContinue();
                c.EnableShutdown();

                c.StartAutomaticallyDelayed();
                c.RunAsLocalSystem();
            });
        }
    }
}