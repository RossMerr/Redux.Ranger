﻿using Autofac;
using Bootstrap;
using Bootstrap.Autofac;
using Common.Logging;
using Common.Logging.Configuration;
using Topshelf;
using Topshelf.Autofac;

namespace Redux.Ranger.Microservice
{
    public class Application
    {
        private static readonly ILog Log = LogManager.GetLogger<Application>();

        public static event ServiceEventHandler ServiceStart;
        public static event ServiceEventHandler ServiceStop;

        static Application()
        {
            ServiceStart += EventServiceCatcher;
            ServiceStop += EventServiceCatcher;
        }

        static void EventServiceCatcher() { }

        public static void Main()
        {
            Log.Info("Bootstrapping ");

            Bootstrapper.With.Autofac().Start();

            Log.Info("Bootstrapped");

            var container = Bootstrapper.Container as Autofac.Core.Container;

            var config = container.Resolve<MicroserviceConfiguration>();

            var name = System.Reflection.Assembly.GetEntryAssembly().GetName();

            // create properties
            var properties = new NameValueCollection();
            properties["showDateTime"] = "true";

            // set Adapter
            LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);

            var serviceControl = container.Resolve<Service>();

            serviceControl.ServiceStart += ServiceStart;
            serviceControl.ServiceStop += ServiceStop;

            HostFactory.Run(c =>
            {
                // Pass it to Topshelf
                c.UseAutofacContainer(container);
                //c.UseCommonLogging();

                c.Service<Service>(s =>
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