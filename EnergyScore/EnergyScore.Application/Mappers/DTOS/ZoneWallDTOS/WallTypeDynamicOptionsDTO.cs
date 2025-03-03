
namespace EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS
{
    public class WallTypeDynamicOptionsDTO
    {
        public Guid Id { get; set; }
        public bool? ExpandedPolystyreneSheathing { get; set; }
        public bool? OptimumValueEngineering { get; set; }
        public string? FramingType { get; set; }
        public bool? Staggered { get; set; }
    }
}
