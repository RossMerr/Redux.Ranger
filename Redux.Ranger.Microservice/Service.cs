using System;
using Common.Logging;
using Microsoft.Owin.Hosting;
using Redux.Ranger.Microservice.Configs;
using Topshelf;

namespace Redux.Ranger.Microservice
{
    public delegate void ServiceEventHandler();
    
    internal class Service : ServiceControl
    {
        private readonly ILog _log = LogManager.GetLogger<Service>();

        private readonly RegisterService _registerService;
        private readonly IService _service;

        public event ServiceEventHandler ServiceStart;
        public event ServiceEventHandler ServiceStop;
        private readonly Uri _uri;

        public Service(Uri uri, RegisterService registerService, IService service)
        {
            _uri = uri;
            _registerService = registerService;
            _service = service;
            ServiceStart += BaseStart;
            ServiceStop += BaseStop;
        }


        void BaseStart()
        {
            _service.Start();
        }

        void BaseStop()
        {
            _service.Stop();
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

            _registerService.Start();

            ServiceStart();
            
            _log.Info("Started");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Stopping");

            if (WebAppHolder == null) return true;

            WebAppHolder.Dispose();
            WebAppHolder = null;

            _registerService.Stop();

            ServiceStop();

            _log.Info("Stopped");

            return true;
        }
    }
}
