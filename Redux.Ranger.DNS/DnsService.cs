using System.Net;
using System.Net.Sockets;
using ARSoft.Tools.Net.Dns;
using ARSoft.Tools.Net.Dns.DynamicUpdate;
using Common.Logging;
using Redux.Ranger.Microservice;

namespace Redux.Ranger.DNS
{
    public class DnsService : IService
    {
        private static readonly ILog Log = LogManager.GetLogger<DnsService>();
        private readonly DnsServer _server;
        private readonly IPAddress _dnsAddress;

        public DnsService()
        {
            _dnsAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

            _server = new DnsServer(_dnsAddress, 10, 10, ProcessQuery);
        }

        public void Start()
        {
            Log.Info("Starting");

            _server.Start();
            GetValue()
            Log.Info("Started");
        }

        public void Stop()
        {
            Log.Info("Stopping");

            _server.Stop();

            Log.Info("Stopped");
        }

        private void GetValue()
        {
            var temp = IPAddress.Broadcast;
            var client = new DnsClient(_dnsAddress, 10000);
            DnsMessage dnsMessage = client.Resolve("www.example.com", RecordType.A);

            var msg = new DnsUpdateMessage
            {
                ZoneName = "example.com"
            };


            msg.Updates.Add(new DeleteRecordUpdate("dyn.example.com", RecordType.A));
            msg.Updates.Add(new AddRecordUpdate(new ARecord("dyn.example.com", 300, IPAddress.Any)));
            //msg.TSigOptions = new TSigRecord("my-key", TSigAlgorithm.Md5, DateTime.Now, new TimeSpan(0, 5, 0), msg.TransactionID,ReturnCode.NoError, null, Convert.FromBase64String("0jnu3SdsMvzzlmTDPYRceA=="));

            Log.InfoFormat("DNS Address: {0}", IPAddress.Any.ToString());
            //var client = new DnsClient(IPAddress.IPv6Any, 5000);

            DnsUpdateMessage dnsResult = client.SendUpdate(msg);
        }

        private DnsMessageBase ProcessQuery(DnsMessageBase message, IPAddress clientaddress, ProtocolType protocoltype)
        {
            message.IsQuery = false;

            var query = message as DnsMessage;

            if ((query != null) && (query.Questions.Count == 1))
            {

                return SendQueryToUpstreamServer(query);
            }

            // Not a valid query
            message.ReturnCode = ReturnCode.ServerFailure;
            return message;
        }

        private DnsMessageBase SendQueryToUpstreamServer(DnsMessage query)
        {
            var question = query.Questions[0];
            var answer = DnsClient.Default.Resolve(question.Name, question.RecordType, question.RecordClass);

            // if got an answer, copy it to the message sent to the client
            if (answer != null)
            {
                foreach (var record in (answer.AnswerRecords))
                {
                    query.AnswerRecords.Add(record);
                }
                foreach (var record in (answer.AdditionalRecords))
                {
                    query.AnswerRecords.Add(record);
                }

                query.ReturnCode = ReturnCode.NoError;
                {
                    return query;

                }
            }

            // upstream server did not answer correct
            query.ReturnCode = ReturnCode.ServerFailure;
            return query;
        }
    }
}
