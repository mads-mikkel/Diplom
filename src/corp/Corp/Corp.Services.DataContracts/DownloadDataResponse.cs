using System.Runtime.Serialization;

namespace Corp.Services.DataContracts
{
    [DataContract]
    public class DownloadDataResponse
    {
        [DataMember(Order = 1)]
        public virtual byte[] Data { get; set; }

        [DataMember(Order = 2)]
        public virtual string Message { get; set; }
    }
}