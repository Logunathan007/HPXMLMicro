

using EnergyScore.Domain.Entityies.CommonModels;

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
        public ICollection<Insulation> Insulations { get; set; }
        public Guid BuildingId { get; set; }
    }
}
