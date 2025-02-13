using EnergyScore.Application.Mappers.DTOS.CommonDTOS;

namespace EnergyScore.Application.Mappers.DTOS.ZoneFloorDTOS
{
    public class SlabDTO
    {
        public Guid Id { get; set; }
        public string SlabName { get; set; }
        public double ExposedPerimeter { get; set; }
        public ICollection<PerimeterInsulationDTO> PerimeterInsulations { get; set; }
    }
}
