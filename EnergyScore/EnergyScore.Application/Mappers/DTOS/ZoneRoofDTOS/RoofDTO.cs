

using EnergyScore.Application.Mappers.DTOS.CommonDTOS;

namespace EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS
{
    public class RoofDTO
    {
        public Guid Id { get; set; }
        public string RoofName { get; set; }
        public string RoofType { get; set; }
        public string RoofColor { get; set; }
        public double Area { get; set; }
        public bool RadiantBarrier { get; set; }
        public double SolarAbsorptance { get; set; }
        public ICollection<InsulationDTO> Insulations { get; set; }
        public ICollection<SkylightDTO> Skylights { get; set; }
        public Guid BuildingId { get; set; }
    }
}
