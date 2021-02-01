using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Corp.Services.SignalRHub
{
    public interface IFloodingAlerter
    {
        Task ShowLatestAlert(string alert); // The method to be invoked on SignalR clients.
    }

    public class FloodingAlerterHub: Hub<IFloodingAlerter> { }  // Marker class
}