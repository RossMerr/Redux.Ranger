using System.Net;
using ARSoft.Tools.Net.Dns;
using Autofac;
using Bootstrap;
using Bootstrap.Autofac;
using Common.Logging;
using Common.Logging.Configuration;
using Redux.Ranger.Microservice;
using Topshelf;
using Topshelf.Autofac;

namespace Redux.Ranger.DNS
{
    public class Application 
    {
        public static void Main()
        {
            Redux.Ranger.Microservice.Application.Main();
        }    
    }
}