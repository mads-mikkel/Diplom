using Corp.Services.Contracts;
using Corp.Services.DataContracts;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Threading.Tasks;

namespace Corp.Tests.Terminal
{
    class Program
    {
        static string address = "http://localhost:9002";
        static string url = "https://kystatlas.kyst.dk/public2/data/vandstand/response.aspx?ident=20201&startdate=20180825&enddate=20180826&format=csv";

        static async Task Main(string[] args)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            using(var channel = GrpcChannel.ForAddress(address))
            {
                var dataAccessService = channel.CreateGrpcService<IDownloadDataService>();
                var request = new DownloadDataRequest() { Uri = new Uri(url) };
                var result = dataAccessService.DownloadWith(request);
                Console.WriteLine(result.Message);
            }
            Console.ReadLine();
        }
    }
}
