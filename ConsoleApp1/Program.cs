using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Runtime;

namespace ConsoleApp1
{
    public class Program
    {
		private readonly IServiceProvider _serviceProvider;

		public Program(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}
		

	    public void Main(string[] args)
	    {
		    //var pro = new Microsoft.AspNet.Hosting.Program(_serviceProvider);
		    //pro.Main(args);

			var configuration1 = new Configuration();
			if (File.Exists("Microsoft.AspNet.Hosting.ini"))
				configuration1.AddIniFile("Microsoft.AspNet.Hosting.ini");
			configuration1.AddEnvironmentVariables();
			configuration1.AddCommandLine(args);
			var serviceProvider1 = HostingServices.Create(_serviceProvider, configuration1).BuildServiceProvider();
			var iapplicationEnvironment = serviceProvider1.GetRequiredService<IApplicationEnvironment>();
			var ihostingEnvironment = serviceProvider1.GetRequiredService<IHostingEnvironment>();
			var hostingContext = new HostingContext();
			hostingContext.Services = serviceProvider1;
			hostingContext.Configuration = configuration1;
			hostingContext.ServerName = configuration1.Get("server");
			hostingContext.ApplicationName = configuration1.Get("app") ?? iapplicationEnvironment.ApplicationName;
			var environmentName = ihostingEnvironment.EnvironmentName;
			hostingContext.EnvironmentName = environmentName;
			var hostingEngine = serviceProvider1.GetRequiredService<IHostingEngine>();
			var appShutdownService = serviceProvider1.GetRequiredService<IApplicationShutdown>();
			var shutdownHandle = new ManualResetEvent(false);
			var serverShutdown = hostingEngine.Start(hostingContext);
			appShutdownService.ShutdownRequested.Register(() =>
			{
				serverShutdown.Dispose();
				shutdownHandle.Set();
			});
			Task.Run(() =>
			{
				Console.WriteLine("Started");
				Console.ReadLine();
				appShutdownService.RequestShutdown();
			});
			shutdownHandle.WaitOne();
			//    var server = new Microsoft.AspNet.Hosting.Startup.StartupManager()

			//Microsoft.AspNet.Hosting--server Microsoft.AspNet.Server.WebListener--server.urls http://localhost:5000
			Console.WriteLine("Hello World");
            Console.ReadLine();
        }
    }
}
