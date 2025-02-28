
using System.Xml.Serialization;

namespace EnergyScore.Application.Templates.HPXMLs.ZoneWalls
{
    public class Windows
    {
        [XmlElement]
        public List<Window> Window { get; set; }    
    }
    public class Window
    {
        public SystemIdentifier SystemIdentifier { get; set; }
        public double Area { get; set; }
        public FrameType FrameType {  get; set; }
        public double UFactor { get; set; }
        public double SHGC { get; set; }
    }
    public class FrameType
    {
        public Aluminum? Aluminum { get; set; }
        public Composite? Composite { get; set; }
        public Fiberglass? Fiberglass { get; set; }
        public Metal? Metal { get; set; }
        public Vinyl? Vinyl { get; set; }
        public Wood? Wood { get; set; }
        public Other? Other { get; set; }
    }
    public class Aluminum
    {
        public bool? ThermalBreak { get; set; }
    }
    public class Metal
    { 
        public bool? ThermalBreak { get; set; }
    }
    public class Composite{}
    public class Fiberglass{}
    public class Vinyl{}
    public class Wood{}

}

