

using EnergyScore.Application.Templates.HPXMLs;

namespace EnergyScore.Application.Templates.Responses
{
    public class Response
    {
        public bool Failed { get; set; }
        public string Message { get; set; }
    }

    public class ResponseForBuilding : Response
    {
        public Guid BuildingId { get; set; }
    }
    public class ResponseForAbout : Response
    {
        public Guid AboutId { get; set; }
    }
    public class ResponseForAddress : Response
    {
        public Guid AddressId { get; set; }
        public Guid? BuildingId { get; set; } = null;
    }
    public class HPXMLGenerationResponse : Response
    {
        public HPXML hPXML { get; set; }
    }
    public class HPXMLStringResponse : Response
    {
        public string HPXMLString { get; set; }
    }
    public class HPXMLBase64Response : Response
    {
        public string HPXMLBase64String { get; set; }
    }
    public class ResponseForZoneFloor : Response
    {
        public Guid ZoneFloorId { get; set; }
    }
}
