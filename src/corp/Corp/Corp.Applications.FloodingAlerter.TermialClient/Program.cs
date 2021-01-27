using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using static Corp.Resources.Infrastructure.Endpoints.Services;

namespace Corp.Applications.FloodingAlerter.TermialClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Client c = new Client() { Id = "1" };
            await c.Initialize();
            Console.ReadLine();
        }
    }

    class Client
    {
        HubConnection connection;
        readonly string url = $"http://localhost:{HubPort}/hubs/clock";

        public string Id { get; set; }

        public async Task Initialize()
        {
            connection = new HubConnectionBuilder().WithUrl(url).Build();
            connection.On<DateTime>(nameof(ShowTime), ShowTime);
            await connection.StartAsync();
        }

        public Task ShowTime(DateTime currentTime)
        {
            Console.WriteLine($"Client {Id} modtaget data kl. {currentTime.ToShortTimeString()}.");
            return Task.CompletedTask;
        }
    }
}
