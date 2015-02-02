using MediatR;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger.Microservice.Register
{
    public class UnregisterHandler : RequestHandler<Stop>
    {
        protected override void HandleCore(Stop message)
        {
           // throw new NotImplementedException();
        }
    }
}
