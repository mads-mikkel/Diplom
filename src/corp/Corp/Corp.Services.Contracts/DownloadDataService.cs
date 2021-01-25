using Corp.Services.DataContracts;
using System;
using System.Net;
using System.ServiceModel;  // Uses protobuf-net.Grpc package. Do not know why. 

namespace Services.ServiceContracts
{
    [ServiceContract(Name = "DownloadDataService")]
    public interface IDownloadDataService
    {
        [OperationContract]
        DownloadDataResponse DownloadWith(DownloadDataRequest request);
    }

    public class DownloadFileService: IDownloadDataService
    {
        public DownloadDataResponse DownloadWith(DownloadDataRequest request)
        {
            DownloadDataResponse response = new();

            try
            {
                using(WebClient webClient = new())
                {
                    response.Data = webClient.DownloadData(request.Uri);
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