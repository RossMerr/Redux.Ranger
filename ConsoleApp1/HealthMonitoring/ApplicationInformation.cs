using System.Net;

namespace ConsoleApp1.HealthMonitoring
{
    internal class ApplicationInformation : IExternalComponentChecker
    {
        public string Message => "Started";

	    public string Name => "ApplicationInformation";

	    public HttpStatusCode StatusCode => HttpStatusCode.OK;
    }
}
