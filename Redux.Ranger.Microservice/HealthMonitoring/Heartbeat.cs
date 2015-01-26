using System;
using System.Globalization;

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

        public int StatusCode
        {
            get { throw new NotImplementedException(); }
        }
    }
}
