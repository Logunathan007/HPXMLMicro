
using EnergyScore.Application.Mappers.DTOS.CommonDTOS;
using EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS;
using EnergyScore.Domain.Entityies.ZoneWallModels;

namespace EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS
{
    public class WallDTO
    {
        public Guid Id { get; set; }
        public string WallName { get; set; }
        public string? AtticWallType { get; set; }
        public double Area { get; set; }
        public string? ExteriorAdjacentTo { get; set; }
        public string? WallType { get; set; }
        public WallTypeDynamicOptionsDTO? WallTypeDynamicOptions { get; set; }
        public string? Siding { get; set; }
        public int? Azimuth { get; set; }
        public string? Orientation { get; set; }
        public ICollection<InsulationDTO>? Insulations { get; set; }
        public ICollection<WindowDTO>? Windows { get; set; }
        public Guid BuildingId { get; set; }
    }
}
