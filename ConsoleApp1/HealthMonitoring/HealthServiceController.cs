using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace ConsoleApp1.HealthMonitoring
{
	[Route("api/[controller]")]
	public class HealthServiceController : Controller
    {
        private readonly IEnumerable<IExternalComponentChecker> _componentCheckers;

        public HealthServiceController(IEnumerable<IExternalComponentChecker> componentCheckers)
        {
            _componentCheckers = componentCheckers;
        }

		[HttpGet]
		public IEnumerable<IExternalComponentChecker> GetAll()
        {		
            return _componentCheckers;
        }
    }
}
