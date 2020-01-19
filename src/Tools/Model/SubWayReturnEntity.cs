using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    [DataContract]
    public class SubWayReturnEntity
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "list")]
        public List<BusData> busData { get; set; }
    }

    [Serializable]
    [DataContract]
    public class BusData
    {
        [DataMember(Name = "id")]
        public string id { get; set; }
        [DataMember(Name = "location")]
        public Location location { get; set; }
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "sequence")]
        public string sequence { get; set; }
        [DataMember(Name = "bus_type")]
        public string bus_type { get; set; }
        [DataMember(Name = "markerType")]
        public string markerType { get; set; }
        [DataMember(Name = "tType")]
        public string tType { get; set; }

    }
    [Serializable]
    [DataContract]
    public class Location
    {
        [DataMember(Name = "lat")]
        public string lat { get; set; }
        [DataMember(Name = "lng")]
        public string lng { get; set; }
    }
}
