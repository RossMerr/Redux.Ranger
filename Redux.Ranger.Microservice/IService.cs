
namespace Redux.Ranger.Microservice
{
    public interface IService
    {
        void Start();
        void Stop();
    }

    internal class BaseService : IService
    {
        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
