using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Redux.Ranger.Microservice.HealthMonitoring
{
    public class HealthServiceController : ApiController
    {
        private readonly IEnumerable<IExternalComponentChecker> _componentCheckers;

        public HealthServiceController(IEnumerable<IExternalComponentChecker> componentCheckers)
        {
            _componentCheckers = componentCheckers;
        }

        public IQueryable<IExternalComponentChecker> Get()
        {
            return _componentCheckers.AsQueryable();
        }
    }
}
