
using EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS;

namespace EnergyScore.Application.Mappers.DTOS.ZoneRoofDTOS
{
    public class AtticDTO
    {
        public Guid Id { get; set; }
        public string AtticName { get; set; }
        public string AtticType { get; set; }
        public AtticTypeDynamicOptionDTO AtticTypeDynamicOptions { get; set; }
        public ICollection<RoofDTO> Roofs { get; set; }
        public ICollection<WallDTO> Walls { get; set; }
        public ICollection<FrameFloorDTO> FrameFloors { get; set; }
    }
}
