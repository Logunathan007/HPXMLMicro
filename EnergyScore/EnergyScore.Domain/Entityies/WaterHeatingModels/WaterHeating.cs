
namespace EnergyScore.Domain.Entityies.WaterHeatingModels
{
    public class WaterHeating
    {
        public Guid Id { get; set; }
        public ICollection<WaterHeatingSystem>? WaterHeatingSystems { get; set; }
    }
}
