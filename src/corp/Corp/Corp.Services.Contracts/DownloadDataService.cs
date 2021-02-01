using Corp.Services.DataContracts;
using System;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Corp.Services.Contracts
{
    [ServiceContract]
    public interface IDownloadDataService
    {
        [OperationContract]
        Task<DownloadDataResponse> DownloadWith(DownloadDataRequest request);
    }

    public class DownloadDataService: IDownloadDataService
    {
        public async Task<DownloadDataResponse> DownloadWith(DownloadDataRequest request)
        {
            DownloadDataResponse response = new();

            try
            {
                using(WebClient webClient = new())
                {
                    response.Data = await webClient.DownloadDataTaskAsync(request.Uri);
                }
            }
            catch(Exception e)
            {
                response.Message = $"An error occurred while attempting to download file from {request.Uri}:{Environment.NewLine}{e.Message}";
            }

            return response;
        }
    }
}