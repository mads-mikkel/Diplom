using Corp.Services.DataContracts;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using static Corp.Resources.Infrastructure.Endpoints.Services;

namespace Corp.Services.Contracts
{
    [ServiceContract]
    public interface IFloodingAlertWorkflowService
    {
        [OperationContract]
        Task<FloodingAlertWorkflowResponse> StartWorkflow();
    }

    public class FloodingAlertWorkflowService: IFloodingAlertWorkflowService
    {
        public async Task<FloodingAlertWorkflowResponse> StartWorkflow()
        {
            FloodingAlertWorkflowResponse response = new();
            try
            {
                DownloadDataResponse downloadDataResponse = await GetCurrentWaterLevelData();
                response.WaterLevel = downloadDataResponse.Data[0];
                response.MessageInfo = "Request succeeded.";
            }
            catch(Exception e)
            {
                response.MessageInfo= "Request failed.";
            }
            return response;
        }

        private async Task<DownloadDataResponse> GetCurrentWaterLevelData()
        {
            string url = GenerateCoastDirectorateUrl();
            Uri uri = new Uri(url);
            DownloadDataRequest request = new DownloadDataRequest() { Uri = uri };
            string localHostAddress = $"http://localhost:{DataAccessServicePort}";
            GrpcChannel channel = GrpcChannel.ForAddress(localHostAddress);
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            DownloadDataResponse response;
            using(channel)
            {
                IDownloadDataService downloadDataService = channel.CreateGrpcService<IDownloadDataService>();
                response = await downloadDataService.DownloadWith(request);
            }
            return response;
        }

        private string GenerateCoastDirectorateUrl()
        {
            string baseUrl = "https://kystatlas.kyst.dk/public2/data/vandstand/response.aspx?";
            string station = "6701"; // Ribe;
            string startDate = DateTime.Today.ToString("yyyyMMdd");
            string endDate = DateTime.Today.AddDays(1).ToString("yyyyMMdd");
            string format = "csv";
            string stationAndDates = $"ident={station}&startdate={startDate}&enddate={endDate}&format={format}";
            return $"{baseUrl}{stationAndDates}";
        }
    }
}