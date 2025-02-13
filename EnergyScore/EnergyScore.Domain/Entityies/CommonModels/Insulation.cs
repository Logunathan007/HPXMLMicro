
using EnergyScore.Domain.Entityies.ZoneFloorModels;
using System.ComponentModel.DataAnnotations;

namespace EnergyScore.Domain.Entityies.CommonModels
{
    public class Insulation
    {
        [Key]
        public Guid Id { get; set; }
        public double NominalRValue { get; set; }
        public double AssemblyEffectiveRValue { get; set; }
        public Guid? FrameFloorId { get; set; }
        public FrameFloor? FrameFloors  { get; set; }
        public Guid? FoundationWallId { get; set; }
        public FoundationWall? FoundationWalls { get; set; }
    }
}
