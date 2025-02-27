
namespace EnergyScore.Application.Mappers.DTOS.WaterHeating { 
    public class WaterHeatingDTO
    {
        public Guid Id { get; set; }
        public ICollection<WaterHeatingSystemDTO>? WaterHeatingSystems { get; set; }
    }
}
