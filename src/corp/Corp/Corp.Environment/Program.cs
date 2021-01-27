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
        //const string appNamespace = "corp.applications.floodingalerter";
        const string fileType = ".exe";
        const int numberOfClients = 3;

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


            //string[] applicationsToStart =
            //{
            //    nameof(Applications.FloodingAlerter.TermialClient)
            //};

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
                StartProcess(fullPath);
                WriteLine($"{service} startet.");
            }
        }


        //static void StartApplications(string[] applicationsToStart)
        //{
        //    WriteLine("Starter services.");
        //    foreach(string app in applicationsToStart)
        //    {
        //        string fullPath = GetFullPathFor(appNamespace, app);
        //        WriteLine($"Starter {app} i {fullPath}...");
        //        for(int i = 0; i < numberOfClients; i++)
        //        {
        //            StartProcess(fullPath, $"{i}");
        //        }
        //        WriteLine($"{app} startet.");
        //    }
        //}

        static void StartProcess(string fullPath, string clientId = null)
        {
            Process p = new Process();
            p.StartInfo.FileName = fullPath;
            if(clientId != null)
            {
                p.StartInfo.Arguments = $"-{clientId}";
            }
            ThreadStart threadStart = new ThreadStart(() => p.Start());
            Thread thread = new Thread(threadStart);
            thread.Start();
        }
    }
}
