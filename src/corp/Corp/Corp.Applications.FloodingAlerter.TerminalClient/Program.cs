using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Corp.Applications.FloodingAlerter.TerminalClient
{
    class Program
    {
        static string url = "http://localhost:9001/floodingalerterhub";

        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder().WithUrl(url).Build();
            await Connect(connection);
            Console.ReadLine();
        }

        static async Task Connect(HubConnection connection)
        {
            connection.On<string>("NewMessage", (messageString) =>
            {
                var message = JsonConvert.DeserializeObject<string>(messageString);
                Console.WriteLine(message);
            });

            await connection.StartAsync();

        }
    }
}