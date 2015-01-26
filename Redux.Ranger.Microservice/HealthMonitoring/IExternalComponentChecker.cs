
namespace Redux.Ranger.Microservice.HealthMonitoring
{
    public interface IExternalComponentChecker
    {
        string Message { get; }
        string Name { get; }
        int StatusCode { get; }
    }
}
