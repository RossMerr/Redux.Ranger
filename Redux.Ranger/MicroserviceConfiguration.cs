using System.Net;

namespace Redux.Ranger
{
    public class MicroserviceConfiguration
    {
        public MicroserviceConfiguration(IPAddress dnsIpAddress, int port)
        {
            Port = port;
            DnsIpAddress = dnsIpAddress;

        }
        public string Name { get; private set; }

        public string Description { get; private set; }

        public int Port { get; private set; }

        public IPAddress DnsIpAddress { get; private set; }

    }
}
