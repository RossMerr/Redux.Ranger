
using System.Net;

namespace ConsoleApp1.HealthMonitoring
{
    public interface IExternalComponentChecker
    {
        string Message { get; }
        string Name { get; }
        HttpStatusCode StatusCode { get; }
    }
}
