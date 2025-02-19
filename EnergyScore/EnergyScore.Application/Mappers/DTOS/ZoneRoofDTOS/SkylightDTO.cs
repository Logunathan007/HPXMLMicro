
namespace EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS
{
    public class SkylightDTO
    {
        public Guid Id { get; set; }
        public double Area { get; set; }
        public double SHGC { get; set; }
        public double UFactor { get; set; }
    }
}
