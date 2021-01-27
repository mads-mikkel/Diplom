using Corp.Services.DataContracts;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using Xunit;

namespace Corp.Test.Services
{
    public class DataAccessServiceTests
    {
        private GrpcChannel

        [Fact]
        public void CanDownloadData()
        {
            // Arrange:
            string url = "https://kystatlas.kyst.dk/public2/data/vandstand/response.aspx?ident=20201&startdate=20180825&enddate=20180826&format=csv";
            Uri uri = new(url);
            DownloadDataRequest request = new() { Uri = uri };

            // Act:
            //DownloadDataResponse response = 

            // Assert:
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            using(var channel = GrpcChannel.ForAddress("http://localhost:10042"))
            {
                var calculator = channel.CreateGrpcService<ICalculator>();
                var result = await calculator.MultiplyAsync(new MultiplyRequest { X = 12, Y = 4 });
                Console.WriteLine(result.Result);
            }
        }
    }
}
