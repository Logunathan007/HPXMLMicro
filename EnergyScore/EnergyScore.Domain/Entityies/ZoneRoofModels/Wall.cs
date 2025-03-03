
using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.CommonModels;
using EnergyScore.Domain.Entityies.ZoneWallModels;

namespace EnergyScore.Domain.Entityies.ZoneRoofModels
{
    public class Wall
    {
        public Guid Id { get; set; }
        public string WallName { get; set; }
        public string? AtticWallType { get; set; }
        public double Area { get; set; }
        public string? ExteriorAdjacentTo { get; set; }
        public string? WallType { get; set; }
        public WallTypeDynamicOptions? WallTypeDynamicOptions { get; set; }
        public string? Siding { get; set; }
        public int? Azimuth { get; set; }
        public string? Orientation { get; set; }
        public Insulation? Insulation { get; set; }
        public ICollection<Window>? Windows { get; set; }
        public Guid? AtticId { get; set; }
        public Attic? Attics { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
