using System.Runtime.Serialization;

namespace Corp.Services.DataContracts
{
    [DataContract]
    public class CsvFilterRequest
    {
        [DataMember(Order = 1)]
        public string Csv { get; set; }

        [DataMember(Order = 2)]
        public int[] KeepColumns{ get; set; }

        [DataMember(Order = 3)]
        public bool RemoveHeader { get; set; }
    }
}