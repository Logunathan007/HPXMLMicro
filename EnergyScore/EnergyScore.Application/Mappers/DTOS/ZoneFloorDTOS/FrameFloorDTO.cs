using EnergyScore.Application.Mappers.DTOS.CommonDTOS;

namespace EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS
{
    public class FrameFloorDTO
    {
        public Guid Id { get; set; }
        public string FrameFloorName { get; set; }
        public double Area { get; set; }
        public ICollection<InsulationDTO> Insulations { get; set; }
    }
}
