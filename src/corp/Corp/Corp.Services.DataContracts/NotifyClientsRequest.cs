using System.Runtime.Serialization;

namespace Corp.Services.DataContracts
{
    [DataContract]
    public class NotifyClientsRequest
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }

        [DataMember(Order = 2)]
        public string HubName { get; set; }
    }
}