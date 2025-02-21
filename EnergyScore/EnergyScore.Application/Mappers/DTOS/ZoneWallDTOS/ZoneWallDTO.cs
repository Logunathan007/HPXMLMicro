using EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS;

namespace EnergyScore.Application.Mappers.DTOS.ZoneWallDTOS
{
    public class ZoneWallDTO
    {
        public Guid Id { get; set; }
        public ICollection<WallDTO> Walls { get; set; }
    }
}
