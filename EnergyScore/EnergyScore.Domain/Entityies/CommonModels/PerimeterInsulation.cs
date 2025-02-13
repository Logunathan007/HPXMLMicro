
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.CommonModels
{
    public class PerimeterInsulation
    {
        [Key]
        public Guid Id { get; set; }
        public double NominalRValue { get; set; }
        public double AssemblyEffectiveRValue { get; set; }
        public Guid? SlabId { get; set; }
        public Slab? Slabs { get; set; }
    }
}
