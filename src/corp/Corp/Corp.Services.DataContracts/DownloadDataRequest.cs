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
}