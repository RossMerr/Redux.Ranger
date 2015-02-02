using System.Net;
using System.Net.Sockets;
using ARSoft.Tools.Net.Dns;
using Common.Logging;
using MediatR;
using Redux.Ranger.Microservice.Notification;

namespace Redux.Ranger.DNS
{
    public class StartHandler : RequestHandler<Start>
    {
        private static readonly ILog Log = LogManager.GetLogger<StartHandler>();
        private readonly DnsServer _server;
        private readonly IPAddress _dnsAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

        public StartHandler(DnsServer server)
        {
            _server = server;

        }

        protected override void HandleCore(Start message)
        {
            _server.Start();
            //GetValue();
        }

        //        private void GetValue()
        //        {
        //            var temp = IPAddress.Broadcast;
        //            var client = new DnsClient(_dnsAddress, 10000);
        //            DnsMessage dnsMessage = client.Resolve("www.example.com", RecordType.A);

        //            var msg = new DnsUpdateMessage
        //            {
        //                ZoneName = "example.com"
        //            };


        //            msg.Updates.Add(new DeleteRecordUpdate("dyn.example.com", RecordType.A));
        //            msg.Updates.Add(new AddRecordUpdate(new ARecord("dyn.example.com", 300, IPAddress.Any)));
        //            //msg.TSigOptions = new TSigRecord("my-key", TSigAlgorithm.Md5, DateTime.Now, new TimeSpan(0, 5, 0), msg.TransactionID,ReturnCode.NoError, null, Convert.FromBase64String("0jnu3SdsMvzzlmTDPYRceA=="));

        //            Log.InfoFormat("DNS Address: {0}", IPAddress.Any.ToString());
        //            //var client = new DnsClient(IPAddress.IPv6Any, 5000);

        //            DnsUpdateMessage dnsResult = client.SendUpdate(msg);
        //        }


        public DnsMessageBase ProcessQuery(DnsMessageBase message, IPAddress clientaddress, ProtocolType protocoltype)
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
