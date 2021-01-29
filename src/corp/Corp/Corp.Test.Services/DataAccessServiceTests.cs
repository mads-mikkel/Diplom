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
    internal static class EnvironmentHelper
    {
        internal static GrpcChannel GetLocalHostChannel(int port)
        {
            string address = $"http://localhost:{port}";
            return GrpcChannel.ForAddress(address);
        }
    }

    public class DataAccessServiceTests
    {

        [Fact]
        public async Task CanDownloadData()
        {
            // Arrange:
            string url = "https://kystatlas.kyst.dk/public2/data/vandstand/response.aspx?ident=20201&startdate=20180825&enddate=20180826&format=csv";
            Uri uri = new(url);
            DownloadDataRequest request = new() { Uri = uri };
            GrpcChannel channel = EnvironmentHelper.GetLocalHostChannel(DataAccessServicePort);
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

    public class FilterServiceTests
    {
        [Fact]
        public async Task CanFilterCsv()
        {
            // Arrange:
            string csv = "data1,data2,data3,data4,data5\n" +
                "data1,data2,data3,data4,data5\n" +
                "data1,data2,data3,data4,data5\n";
            GrpcChannel channel = EnvironmentHelper.GetLocalHostChannel(FilterServicePort);
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            int[] keepColumns = { 1, 2, 4 };
            string filteredCsv;
            string expected = "data2, data3, data5\n" +
                "data2, data3, data5\n" +
                "data2, data3, data5\n";
            CsvFilterRequest request = new() { Csv = csv, KeepColumns = keepColumns };
            using(channel)
            {
                ITextFilterService service = channel.CreateGrpcService<ITextFilterService>();

                // Act:
                filteredCsv = await service.FilterCsvColumns(request);
            }

            // Assert:
            Assert.Equal(expected, csv);
        }
    }
}