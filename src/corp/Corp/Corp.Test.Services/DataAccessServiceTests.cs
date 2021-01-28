using Corp.Services.Contracts;
using Corp.Services.DataContracts;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Threading.Tasks;
using Xunit;
using static Corp.Resources.Infrastructure.Endpoints.Services;

namespace Corp.Test.Services
{
    public class DataAccessServiceTests
    {
        private GrpcChannel GetLocalHostChannel(int port)
        {
            string address = $"http://localhost:{port}";
            return GrpcChannel.ForAddress(address);
        }

        [Fact]
        public async Task CanDownloadData()
        {
            // Arrange:
            string url = "https://kystatlas.kyst.dk/public2/data/vandstand/response.aspx?ident=20201&startdate=20180825&enddate=20180826&format=csv";
            Uri uri = new(url);
            DownloadDataRequest request = new() { Uri = uri };
            GrpcChannel channel = GetLocalHostChannel(DataAccessServicePort);
            DownloadDataResponse response;
            GrpcClientFactory.AllowUnencryptedHttp2 = true;   

            using(channel)
            {
                IDownloadDataService service = channel.CreateGrpcService<IDownloadDataService>();

                // Act:
                response = await service.DownloadWith(request);
            }

            // Assert:
            Assert.NotNull(response.Data);
            Assert.True(response.Data.Length > 0);
        }
    }
}
