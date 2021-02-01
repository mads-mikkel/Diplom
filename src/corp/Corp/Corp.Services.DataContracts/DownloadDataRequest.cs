using System;
using System.Runtime.Serialization;

namespace Corp.Services.DataContracts
{
    [DataContract]
    public class DownloadDataRequest
    {
        [DataMember(Order = 1)]
        public virtual Uri Uri { get; set; }
    }

    [DataContract]
    public class DownloadDataResponse
    {
        [DataMember(Order = 1)]
        public virtual byte[] Data { get; set; }

        [DataMember(Order = 2)]
        public virtual string Message { get; set; }
    }
}