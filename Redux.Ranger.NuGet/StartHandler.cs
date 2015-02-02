using MediatR;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger.NuGet
{
    public class StartHandler : RequestHandler<Start>
    {
        protected override void HandleCore(Start message)
        {
           // throw new NotImplementedException();
        }
    }
}
