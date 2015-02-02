using System.Net;
using System.Net.Sockets;
using ARSoft.Tools.Net.Dns;
using MediatR;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger.DNS
{
    public class StopHandler : RequestHandler<Stop>
    {
        private readonly DnsServer _server;

        public StopHandler(DnsServer server)
        {
            _server = server;
        }

        protected override void HandleCore(Stop message)
        {
           _server.Stop();
        }
    }
}
