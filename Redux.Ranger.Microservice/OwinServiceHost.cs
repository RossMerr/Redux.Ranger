using System;
using Bootstrap;
using Common.Logging;
using Common.Logging.Configuration;
using Topshelf;
using Topshelf.Autofac;

namespace Redux.Ranger.Microservice
{
    public class OwinServiceHost
    {
        private static readonly ILog Log = LogManager.GetLogger<OwinServiceHost>();

        public event ServiceEventHandler ServiceStart;
        public event ServiceEventHandler ServiceStop;
        
        static void EventServiceCatcher() { }

        public OwinServiceHost(Uri uri, IService service)
        {
            Uri = uri;
            Service = service;
            ServiceStart += EventServiceCatcher;
            ServiceStop += EventServiceCatcher;
        }

        public IService Service { get; set; }

        public Uri Uri { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceDescription { get; set; }

        public void Initialize()
        {
            var container = Bootstrapper.Container as Autofac.Core.Container;

            var name = System.Reflection.Assembly.GetEntryAssembly().GetName();

            // create properties
            var properties = new NameValueCollection();
            properties["showDateTime"] = "true";

            // set Adapter
            LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
            
            var serviceControl = new Service(Uri, new RegisterService(), Service); 

            serviceControl.ServiceStart += ServiceStart;
            serviceControl.ServiceStop += ServiceStop;

            HostFactory.Run(c =>
            {
                // Pass it to Topshelf
                c.UseAutofacContainer(container);
                //c.UseCommonLogging();
                
                c.Service<Service>(s =>
                {
                     s.ConstructUsing(() => serviceControl);
                    // Let Topshelf use it
                    //s.ConstructUsingAutofacContainer();
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

                c.SetServiceName(ServiceName);
                c.SetDisplayName(ServiceDisplayName);
                c.SetDescription(ServiceDescription);

                c.EnablePauseAndContinue();
                c.EnableShutdown();

                c.StartAutomaticallyDelayed();
                c.RunAsLocalSystem();
            });
        }
    }
}