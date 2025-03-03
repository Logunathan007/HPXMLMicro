using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.CommonModels;
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.ZoneFloorModels
{
    public class Slab
    {
        [Key]
        public Guid Id { get; set; }
        public string SlabName { get; set; }
        public double ExposedPerimeter { get; set; }
        public Insulation PerimeterInsulation { get; set; }
        public Guid? FoundationId { get; set; }
        public Foundation Foundations { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
