using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bootstrap;

namespace Redux.Ranger
{
    public static class AutoExtensions
    {
        public static Bootstrap.Extensions.BootstrapperExtensions Ranger(
            this Bootstrap.Extensions.BootstrapperExtensions extensions)
        {
            extensions.Including.AndAssembly(Assembly.GetAssembly(typeof (AutoExtensions)));

            return extensions;
        }
    }
}
