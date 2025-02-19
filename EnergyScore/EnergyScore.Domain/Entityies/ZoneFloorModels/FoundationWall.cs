
using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.CommonModels;
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.ZoneFloorModels
{
    public class FoundationWall
    {
        [Key]
        public Guid Id { get; set; }
        public string FoundationWallName { get; set; }
        public double Area { get; set; }
        public ICollection<Insulation> Insulations { get; set; }
        public Guid FoundationId { get; set; }
        public Foundation Foundations { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
