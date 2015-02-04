using System.Reflection;

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
