
using System.Xml.Serialization;

namespace EnergyScore.Domain.HPXMLClasses
{
    [XmlRoot("HPXML", Namespace = "http://hpxmlonline.com/2023/09")]
    public class HPXML
    {
        [XmlAttribute("schemaVersion")]
        public string SchemaVersion { get; set; } = "3.1";
        public XMLTransactionHeaderInformation XMLTransactionHeaderInformation { get; set; }
        public SoftwareInfo SoftwareInfo { get; set; }
        public Building Building { get; set; }
    }

    public class XMLTransactionHeaderInformation
    {
        public string XMLType { get; set; }
        public string XMLGeneratedBy { get; set; }
        public DateTime CreatedDateAndTime { get; set; } = System.DateTime.Now;
        public string Transaction { get; set; }
    }

    public class SoftwareInfo
    {
        public string SoftwareProgramUsed { get; set; } = "EnergyScore From Inspection Depot";
        public string SoftwareProgramVersion { get; set; } = "1.0";
    }

    public class Building
    {
        [XmlElement("BuildingID")]
        public BuildingID BuildingID { get; set; }
        public Site Site { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public BuildingDetails BuildingDetails { get; set; }
    }

    public class BuildingID
    {
        [XmlAttribute("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }

    public class Site
    {
        [XmlElement("SiteID")]
        public SiteID SiteID { get; set; }
        public Address Address { get; set; }
    }

    public class SiteID
    {
        [XmlAttribute("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }

    public class Address
    {
        public string AddressType { get; set; } = "street";
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CityMunicipality { get; set; }
        public string StateCode { get; set; }
        public int ZipCode { get; set; }
    }

    public class ProjectStatus
    {
        public string EventType { get; set; } 
        public DateTime Date { get; set; } 
    }

    public class BuildingDetails
    {
        public BuildingSummary BuildingSummary { get; set; }
    }

    public class BuildingSummary
    {
        public BuildingConstruction BuildingConstruction { get; set; }
    }

    public class BuildingConstruction
    {
        public string ResidentialFacilityType { get; set; } 
    }
}