using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EnergyScore.Application.Templates.HPXMLs.ZoneFloors
{
    public class FrameFloors
    {
        [XmlElement("FrameFloor")]
        public List<FrameFloor> FrameFloor { get; set; }
    }
    public class FrameFloor
    {
        public SystemIdentifier SystemIdentifier { get; set; }
        public double Area { get; set; }
        [XmlElement("Insulation")]
        public List<Insulaion> Insulaion { get; set; }
    }
}
