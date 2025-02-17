using EnergyScore.Domain.Entityies.CommonModels;
using EnergyScore.Domain.Entityies.ZoneRoofModels;
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.ZoneFloorModels
{
    public class FrameFloor
    {
        [Key]
        public Guid Id { get; set; }
        public string FrameFloorName { get; set; }
        public double Area { get; set; }
        public ICollection<Insulation> Insulations { get; set; }
        public Guid? FoundationId { get; set; } = null;
        public Foundation? Foundations { get; set; } = null;
        public Guid? AtticId { get; set; } = null;
        public Attic? Attic { get; set; } = null;

    }
}
