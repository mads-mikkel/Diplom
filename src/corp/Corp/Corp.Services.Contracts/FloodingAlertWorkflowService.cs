using Corp.Services.DataContracts;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.ServiceModel;
using System.Text;
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
                string filterredCsv = await FilterData(downloadDataResponse);
                response.WaterLevel = 25;
                response.MessageInfo = "Request succeeded.";
            }
            catch(Exception e)
            {
                response.MessageInfo = "Request failed.";
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

        private async Task<string> FilterData(DownloadDataResponse dataResponse)
        {
            string localHostAddress = $"http://localhost:{FilterServicePort}";
            GrpcChannel channel = GrpcChannel.ForAddress(localHostAddress);
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            string csv = Encoding.Default.GetString(dataResponse.Data);
            int[] keepColumns = new int[] { 1, 2, 5 };
            CsvFilterRequest filterRequest = new() { Csv = csv, KeepColumns = keepColumns, RemoveHeader = true };
            string response;
            using(channel)
            {
                ITextFilterService textFilter = channel.CreateGrpcService<ITextFilterService>();
                response = await textFilter.FilterCsvColumns(filterRequest);
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