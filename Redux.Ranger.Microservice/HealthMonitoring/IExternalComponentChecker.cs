
using System.Net;

namespace Redux.Ranger.Microservice.HealthMonitoring
{
    public interface IExternalComponentChecker
    {
        string Message { get; }
        string Name { get; }
        HttpStatusCode StatusCode { get; }
    }
}
