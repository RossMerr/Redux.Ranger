using MediatR;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger.NuGet
{
    public class StopHandler : RequestHandler<Stop>
    {
        protected override void HandleCore(Stop message)
        {
           // throw new NotImplementedException();
        }
    }
}
