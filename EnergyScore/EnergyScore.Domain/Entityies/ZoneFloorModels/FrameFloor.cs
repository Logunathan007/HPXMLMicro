using EnergyScore.Domain.Entityies.CommonModels;
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
        public Guid FoundationId { get; set; }
        public Foundation Foundations { get; set; }
    }
}
