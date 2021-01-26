using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace Corp.Applications.FloodingAlerter.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureKestrel(options => options.ListenLocalhost(10042, ListenOptions => ListenOptions.Protocols = HttpProtocols.Http2))
                   .UseStartup<Startup>();
    }
}
