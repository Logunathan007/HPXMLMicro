
using System.Xml.Serialization;

namespace EnergyScore.Application.Templates.HPXMLs
{
    [XmlRoot("HPXML", Namespace = "http://hpxmlonline.com/2019/10")]
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
        public DateTime CreatedDateAndTime { get; set; } = DateTime.Now;
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
        public string Id { get; set; }
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
        public string Id { get; set; }
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
        public Enclosure Enclosure { get; set; }
    }

    public class BuildingSummary
    {
        [XmlElement("Site")]
        public BSSite Site { get; set; }
        public BuildingConstruction BuildingConstruction { get; set; }
    }

    public class BuildingConstruction
    {
        public int YearBuilt { get; set; }
        public string ResidentialFacilityType { get; set; }
        public float NumberofConditionedFloorsAboveGrade { get; set; }
        public float AverageCeilingHeight { get; set; }
        public int NumberofBedrooms { get; set; }
        public float ConditionedFloorArea { get; set; }
        public float ConditionedBuildingVolume { get; set; }
    }

    public class BSSite
    {
        public string OrientationOfFrontOfHome { get; set; }
        public int AzimuthOfFrontOfHome { get; set; }
    }

    public class Enclosure
    {
        public AirInfiltration AirInfiltration { get; set; }
    }

    public class AirInfiltration
    {
        public List<AirInfiltrationMeasurement> AirInfiltrationMeasurement { get; set; }
    }

    public class AirInfiltrationMeasurement
    {
        [XmlElement("id")]
        public SystemIndentifier SystemIndentifier { get; set; }
        public string LeakinessDescription { get; set; }
        public BuildingAirLeakage BuildingAirLeakage { get; set; }
    }

    public class SystemIndentifier
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    public class BuildingAirLeakage
    {
        public string UnitofMeasure { get; set; }
        public float AirLeakage { get; set; }
    }
}