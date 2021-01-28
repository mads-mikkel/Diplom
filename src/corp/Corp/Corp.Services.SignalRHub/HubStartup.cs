using Corp.Resources.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Corp.Resources.Infrastructure.Endpoints;

namespace Corp.Services.SignalRHub
{
    public class HubStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddHostedService<Worker>();
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
                endpoints.MapHub<FloodingAlerterHub>(HubNames.FloodingAlerterHub);
            });
        }
    }
}