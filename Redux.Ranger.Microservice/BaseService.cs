﻿using System;
using Common.Logging;
using Microsoft.Owin.Hosting;
using Redux.Ranger.Client;
using Redux.Ranger.Microservice.Configs;
using Topshelf;

namespace Redux.Ranger.Microservice
{
    internal class BaseService
    {
        private readonly ILog _log = LogManager.GetLogger<BaseService>();

        private readonly MicroserviceConfiguration _configuration;

        public BaseService(MicroserviceConfiguration configuration)
        {
            _configuration = configuration;
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

            _log.Info("Started");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Stopping");

            if (WebAppHolder == null) return true;

            WebAppHolder.Dispose();
            WebAppHolder = null;

            _log.Info("Stopped");

            return true;
        }
    }
}