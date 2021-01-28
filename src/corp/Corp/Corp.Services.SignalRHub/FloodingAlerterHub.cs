using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Corp.Services.SignalRHub
{
    public interface IFloodingAlerter
    {
        Task ShowLatestAlert(string alert); // The method to be invoked on SignalR clients.
    }

    public class FloodingAlerterHub: Hub<IFloodingAlerter>
    {
        public async Task SendAlertToClients(string alert)
        {
            await Clients.All.ShowLatestAlert(alert);   // Push to alert to all the clients.
        }
    }
}