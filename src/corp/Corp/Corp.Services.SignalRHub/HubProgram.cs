using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Corp.Services.SignalRHub
{
    public class HubProgram
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<HubStartup>();
                });
    }
}