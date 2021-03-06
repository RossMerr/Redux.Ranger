﻿using System;
using Bootstrap;
using Bootstrap.Autofac;
using Redux.Ranger.Microservice;

namespace Redux.Ranger.NuGet
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.With.Autofac().With.Start();

            var service = new OwinServiceHost(new Uri("http://localhost/"))
            {
                ServiceName = "NuGet",
                ServiceDisplayName = "NuGet Service",
                ServiceDescription = "NuGet service"
            };
            service.Initialize();
        }
    }
}
