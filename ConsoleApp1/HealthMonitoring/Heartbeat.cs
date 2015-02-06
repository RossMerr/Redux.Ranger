using System;
using System.Globalization;
using System.Net;

namespace ConsoleApp1.HealthMonitoring
{
    internal class Heartbeat : IExternalComponentChecker
    {
        public string Message => DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

	    public string Name => "Heartbeat";

	    public HttpStatusCode StatusCode => HttpStatusCode.OK;
    }
}
