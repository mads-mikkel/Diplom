using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using static Corp.Resources.Infrastructure.Endpoints.Services;

namespace Corp.Applications.FloodingAlerter.Backend
{
    public class BackendProgram
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureKestrel(options => options.ListenLocalhost(FloodingAlerterWorkflowPort, ListenOptions => ListenOptions.Protocols = HttpProtocols.Http2))
                   .UseStartup<BackendStartup>();
    }
}
