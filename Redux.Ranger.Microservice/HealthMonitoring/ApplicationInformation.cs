﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Ranger.Microservice.HealthMonitoring
{
    internal class ApplicationInformation : IExternalComponentChecker
    {
        public string Message
        {
            get { return "Started"; }
        }

        public string Name
        {
            get { return "ApplicationInformation"; }
        }

        public HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.OK; }
        }
    }
}
