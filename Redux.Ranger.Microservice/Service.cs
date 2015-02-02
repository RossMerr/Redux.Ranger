using System;
using Common.Logging;
using MediatR;
using Microsoft.Owin.Hosting;
using Redux.Ranger.Microservice.Configs;
using Redux.Ranger.Microservice.Notification;
using Topshelf;

namespace Redux.Ranger.Microservice
{
    public delegate void ServiceEventHandler();
    
    internal class Service : ServiceControl
    {
        private readonly ILog _log = LogManager.GetLogger<Service>();
        private readonly Uri _uri;
        private readonly IMediator _mediator;

        public Service(Uri uri, IMediator mediator)
        {
            _uri = uri;
            _mediator = mediator;
        }

        protected IDisposable WebAppHolder
        {
            get;
            set;
        }

        public bool Start(HostControl hostControl)
        {
            _log.Info("Starting");
            
            if (WebAppHolder == null)
            {
                WebAppHolder = WebApp.Start
                (
                    new StartOptions
                    {
                        Port = _uri.Port
                    },
                    appBuilder =>
                    {
                        new StartupConfig().Configure(appBuilder);
                    }
                );
                
            }

            _mediator.Send(new Start());
            
            _log.Info("Started");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Stopping");

            if (WebAppHolder == null) return true;

            WebAppHolder.Dispose();
            WebAppHolder = null;

            _mediator.Send(new Stop());

            _log.Info("Stopped");

            return true;
        }
    }
}
