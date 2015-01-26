using System.Net;
using System.Net.Sockets;
using ARSoft.Tools.Net.Dns;
using Common.Logging;
using Topshelf;

namespace Redux.Ranger.DNS
{
    public class BaseService
    {
        private readonly ILog _log = LogManager.GetLogger<BaseService>();
        private readonly DnsServer _server;

        public BaseService()
        {
            _server = new DnsServer(IPAddress.Any, 10, 10, ProcessQuery);
        }

        public bool Start(HostControl hostControl)
        {
            _log.Info("Starting");

            _server.Start();

            _log.InfoFormat("Running on: {0}", IPAddress.Any.ToString());

            _log.Info("Started");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("Stopping");

            _server.Stop();

            _log.Info("Stopped");

            return true;
        }

        private static DnsMessageBase ProcessQuery(DnsMessageBase message, IPAddress clientaddress, ProtocolType protocoltype)
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

        private static DnsMessageBase SendQueryToUpstreamServer(DnsMessage query)
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
