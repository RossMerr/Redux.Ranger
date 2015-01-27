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

        private readonly MicroserviceConfiguration _configuration;
        private readonly RegisterService _registerService;
        private readonly IService _service;

        public event ServiceEventHandler ServiceStart;
        public event ServiceEventHandler ServiceStop;

        public Service(MicroserviceConfiguration configuration, RegisterService registerService, IService service)
        {
            _configuration = configuration;
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

        protected int Port
        {
            get
            {
                return _configuration.Port;
            }
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
                        Port = Port
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
