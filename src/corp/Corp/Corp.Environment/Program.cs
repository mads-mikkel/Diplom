using System;
using System.Diagnostics;
using System.Threading;
using static System.Console;

namespace Corp.Environment
{
    class Program
    {
        const string path = @"C:\users\mara\source\repos\diplom\src\corp\corp\";
        const string folder = @"\bin\debug\net5.0\";
        const string servicesNamespace = "corp.services.";
        const string appNamespace = "corp.applications.";
        const string fileType = ".exe";

        static string GetFullPathFor(string ns, string app) => @$"{path}{ns}{app}{folder}{ns}{app}{fileType}";

        static void Main()
        {
            WriteLine("--- DRIFTSMILJØSIMULATION START ---");
            string[] servicesToStart =
            {
                nameof(Services.DataAccessService),
                nameof(Services.SignalRHub)
            };

            try
            {
                StartServices(servicesToStart);
            }
            catch(Exception e)
            {
                WriteLine(e.Message);
                End();
            }

            void End()
            {
                WriteLine("--- DRIFTSMILJØSIMULATION SLUT ---");
                ReadLine();
                System.Environment.Exit(0);
            }            
        }

        static void StartServices(string[] servicesToStart)
        {
            WriteLine("Starter services.");
            foreach(string service in servicesToStart)
            {
                string fullPath = GetFullPathFor(servicesNamespace, service);
                WriteLine($"Starter {service} i {fullPath}...");
                Process p = new Process();
                p.StartInfo.FileName = fullPath;
                ThreadStart threadStart = new ThreadStart(() => p.Start());
                Thread thread = new Thread(threadStart);
                thread.Start();
                WriteLine($"{service} startet.");
            }
        }
        static void StartApplications() { }
    }
}
