using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Server;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Corp.Applications.FloodingAlerter.Backend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCodeFirstGrpc();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapHub<FloodingAlertHub>("");
            });
        }
    }

    public class FloodingAlertHub :Hub<IAlertClient> 
    {
        public async Task SendMessage(string message) => await Clients.All.ReceiveMessage(message);
    }

    public interface IAlertClient
    {
        Task ReceiveMessage(string message);
    }
}
