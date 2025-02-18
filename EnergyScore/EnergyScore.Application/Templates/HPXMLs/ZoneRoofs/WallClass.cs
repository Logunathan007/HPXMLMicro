
using System.Xml.Serialization;

namespace EnergyScore.Application.Templates.HPXMLs.ZoneRoofs
{
    public class Walls
    {
        [XmlElement]
        public List<Wall> Wall { get; set; }
    }
    public class Wall
    {
        public SystemIdentifier SystemIdentifier { get; set; }
        public string AtticWallType { get; set; }
        public double Area { get; set; }
    }
}
