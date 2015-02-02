using System;
using Common.Logging;
using Common.Logging.Configuration;
using MediatR;
using Microsoft.Practices.ServiceLocation;
using Topshelf;

namespace Redux.Ranger.Microservice
{
    public class OwinServiceHost
    {
        private static readonly ILog Log = LogManager.GetLogger<OwinServiceHost>();

        public OwinServiceHost(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceDescription { get; set; }

        public void Initialize()
        {
            // create properties
            var properties = new NameValueCollection();
            properties["showDateTime"] = "true";

            // set Adapter
            LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);

            var mediator = ServiceLocator.Current.GetInstance<IMediator>();

            var serviceControl = new Service(Uri, mediator); 

            HostFactory.Run(c =>
            {
                // Pass it to Topshelf
           
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