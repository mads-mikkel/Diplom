using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Corp.Services.SignalRHub
{
    public class ClockHub:Hub<IClock>
    {
        public async Task SendTimeToClients(DateTime dateTime)
        {
            await Clients.All.ShowTime(dateTime);
        }
    }
}