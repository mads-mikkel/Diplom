using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using static Corp.Resources.Infrastructure.Endpoints.Services;

namespace Corp.Applications.FloodingAlerter.TermialClient
{
    class TerminalClientProgram
    {
        static async Task Main(string[] args)
        {
            Random generator = new Random();
            string id = generator.Next(1, 100).ToString();
            Client c = new Client() { Id = id };
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