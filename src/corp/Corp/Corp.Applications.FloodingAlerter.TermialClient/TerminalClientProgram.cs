using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using static Corp.Resources.Infrastructure.Endpoints.Services;
using static Corp.Resources.Infrastructure.Endpoints.HubNames;

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
        readonly string url = $"http://localhost:{HubPort}{FloodingAlerterHub}";

        public string Id { get; set; }

        public async Task Initialize()
        {
            connection = new HubConnectionBuilder().WithUrl(url).Build();
            connection.On<string>(nameof(ShowLatestAlert), ShowLatestAlert);
            await connection.StartAsync();
        }

        public Task ShowLatestAlert(string alert)
        {
            Console.WriteLine($"Client {Id}: {alert}");
            return Task.CompletedTask;
        }
    }
}