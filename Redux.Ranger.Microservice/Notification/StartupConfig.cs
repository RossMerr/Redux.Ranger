using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Owin;

namespace Redux.Ranger.Microservice.Notification
{
    public class StartupConfig : IRequest
    {
        public IAppBuilder AppBuilder { get; set; }
        public HttpConfiguration Configuration { get; set; }
    }
}
