using EnergyScore.Domain.Entityies.ZoneRoofModels;

namespace EnergyScore.Domain.Entityies.ZoneWallModels
{
    public class WallTypeDynamicOptions
    {
        public Guid Id { get; set; }
        public bool? ExpandedPolystyreneSheathing { get; set; }
        public bool? OptimumValueEngineering { get; set; }
        public string? FramingType { get; set; }
        public bool? Staggered { get; set; }
        public Guid WallId { get; set; }
        public Wall Wall { get; set; }
    }
}
