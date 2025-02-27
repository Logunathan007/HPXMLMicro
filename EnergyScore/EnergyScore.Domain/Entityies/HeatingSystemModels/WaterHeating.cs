
namespace EnergyScore.Domain.Entityies.HeatingSystemModels
{
    public class WaterHeating
    {
        public Guid Id { get; set; }
        public ICollection<WaterHeatingSystem>? WaterHeatingSystems { get; set; }
    }
}
