using System;
using Common.Logging;
using Microsoft.Owin.Hosting;
using Redux.Ranger.Microservice.Configs;
using Topshelf;

namespace Redux.Ranger.Microservice
{
    public interface IBaseService
    {
        bool Start(HostControl hostControl);
        bool Stop(HostControl hostControl);
    }

    internal class BaseService : IBaseService
    {
        private readonly ILog _log = LogManager.GetLogger<BaseService>();

        private readonly MicroserviceConfiguration _configuration;
        private readonly RegisterService _registerService;

        public BaseService(MicroserviceConfiguration configuration, RegisterService registerService)
        {
            _configuration = configuration;
            _registerService = registerService;
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

            _log.Info("Stopped");

            return true;
        }
    }
}
