using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tools.ZYT
{
    [Serializable]
    [XmlRoot("China")]
    public class ChinaEntity
    {
        [XmlElement("Province")]

        public List<ProvinceEntity> Latitude { get; set; }
    }
    [Serializable]
    public class ProvinceEntity
    {
        [XmlAttribute("Name")]
        public string Name {get; set;}
        [XmlElement("City")]
        public List<CityEntity> City { get;set;}
     }
    [Serializable]
    public class CityEntity
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlElement("District")]
        public List<DistrictEntity> CityChildren { get; set; }
    }
    [Serializable]
    public class DistrictEntity
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Lng")]
        public string Lng { get; set; }
        [XmlAttribute("Lat")]
        public string Lat { get; set; }
    }
}
