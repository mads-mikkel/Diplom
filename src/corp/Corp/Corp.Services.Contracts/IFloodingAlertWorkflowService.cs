using Corp.Services.DataContracts;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Corp.Services.Contracts
{
    [ServiceContract]
    public interface IFloodingAlertWorkflowService
    {
        [OperationContract]
        Task<FloodingAlertWorkflowResponse> StartWorkflow();
    }
}