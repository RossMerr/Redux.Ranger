using System;
using System.Globalization;
using System.Net;

namespace Redux.Ranger.Microservice.HealthMonitoring
{
    internal class Heartbeat : IExternalComponentChecker
    {
        public string Message
        {
            get { return DateTime.UtcNow.ToString(CultureInfo.InvariantCulture); }
        }

        public string Name
        {
            get { return "Heartbeat"; }
        }

        public HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.OK; }
        }
    }
}
