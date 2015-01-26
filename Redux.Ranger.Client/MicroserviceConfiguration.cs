using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.Ranger.Client
{
    public class MicroserviceConfiguration
    {
        public MicroserviceConfiguration(int port)
        {
            Port = port;
        }
        public string Name { get; private set; }

        public string Description { get; private set; }

        public int Port { get; private set; }
    }
}
