using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace ConsoleApp1
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
			app.UseWelcomePage();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}
	}
}