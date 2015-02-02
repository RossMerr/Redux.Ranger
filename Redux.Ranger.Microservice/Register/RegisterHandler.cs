using MediatR;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger.Microservice.Register
{
    public class RegisterHandler : RequestHandler<Start>
    {
        protected override void HandleCore(Start message)
        {

            //var msg = new DnsUpdateMessage
            //{
            //    ZoneName = "example.com"
            //};
            
            //msg.Updates.Add(new DeleteRecordUpdate("dyn.example.com", RecordType.A));
            //msg.Updates.Add(new AddRecordUpdate(new ARecord("dyn.example.com", 300, IPAddress.Any)));
            //msg.TSigOptions = new TSigRecord("my-key", TSigAlgorithm.Md5, DateTime.Now, new TimeSpan(0, 5, 0), msg.TransactionID, ReturnCode.NoError, null, Convert.FromBase64String("0jnu3SdsMvzzlmTDPYRceA=="));

            //_log.InfoFormat("DNS Address: {0}", IPAddress.Any.ToString());

            //var dnsAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];


            //DnsUpdateMessage dnsResult = new DnsClient(dnsAddress, 5000).SendUpdate(msg);
        }
    }
}
