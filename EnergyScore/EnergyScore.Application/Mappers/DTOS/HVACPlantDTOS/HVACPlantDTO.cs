
namespace EnergyScore.Application.Mappers.DTOS.HVACPlantDTOS
{
    public class HVACPlantDTO
    {
        public Guid Id { get; set; }
        public ICollection<HeatingSystemDTO>? HeatingSystems { get; set; }
        public ICollection<CoolingSystemDTO>? CoolingSystems { get; set; }
        public ICollection<HeatPumpDTO>? HeatPumps { get; set; }
    }
}