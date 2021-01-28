using System;
using System.Runtime.Serialization;

namespace Corp.Services.DataContracts
{
    [DataContract]
    public class FloodingAlertWorkflowResponse
    {
        [DataMember(Order = 1)]
        public double WaterLevel { get; set; }

        [DataMember(Order = 2)]
        public string WindSpeed { get; set; }

        [DataMember(Order = 3)]
        public DateTime Time { get; set; }

        [DataMember(Order = 4)]
        public string MessageInfo { get; set; }
    }
}