
using System.Xml.Serialization;

namespace EnergyScore.Application.Templates.HPXMLs.ZoneRoofs
{
    public class Skylights
    {
        [XmlElement]
        public List<Skylight> Skylight { get; set; }
    }
    public class Skylight
    {
        public SystemIdentifier SystemIdentifier { get; set; }
        public double Area { get; set; }
        public double UFactor { get; set; }
        public double SHGC { get; set; }
        [XmlElement]
        public AttachedToRoof AttachedToRoof { get; set; }
    }
}
