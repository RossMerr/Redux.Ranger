using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ARSoft.Tools.Net.Dns;
using ARSoft.Tools.Net.Dns.DynamicUpdate;
using Common.Logging;

namespace Redux.Ranger.Microservice
{
    internal class RegisterService
    {
        private readonly ILog _log = LogManager.GetLogger<RegisterService>();

        private readonly MicroserviceConfiguration _microserviceConfiguration;

        public RegisterService(MicroserviceConfiguration microserviceConfiguration)
        {
            _microserviceConfiguration = microserviceConfiguration;
        }

        public bool Start()
        {

            var msg = new DnsUpdateMessage
            {
                ZoneName = "example.com"
            };

            

            msg.Updates.Add(new DeleteRecordUpdate("dyn.example.com", RecordType.A));
            msg.Updates.Add(new AddRecordUpdate(new ARecord("dyn.example.com", 300, IPAddress.Any)));
            msg.TSigOptions = new TSigRecord("my-key", TSigAlgorithm.Md5, DateTime.Now, new TimeSpan(0, 5, 0), msg.TransactionID, ReturnCode.NoError, null, Convert.FromBase64String("0jnu3SdsMvzzlmTDPYRceA=="));

            _log.InfoFormat("DNS Address: {0}", IPAddress.Any.ToString());

            var dnsAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];


            DnsUpdateMessage dnsResult = new DnsClient(dnsAddress, 5000).SendUpdate(msg);

            return true;
        }

        public bool Stop()
        {
            return true;
        }
    }
}
