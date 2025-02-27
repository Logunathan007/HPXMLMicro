
using EnergyScore.Domain.Entityies.AddressModels;

namespace EnergyScore.Domain.Entityies.HeatingSystemModels
{
    public class WaterHeatingSystem
    {
        public Guid Id { get; set; }
        public string HeatingSystemName { get; set; }
        public double FractionDHWLoadServed { get; set; }
        public string WaterHeaterType { get; set; }
        public string FuelType { get; set; }
        public double EnergyFactor { get; set; }
        public double UniformEnergyFactor { get; set; }
        public Building Building { get; set; }
        public Guid BuildingId { get; set; }
        public WaterHeating WaterHeating{ get; set; }
        public Guid WaterHeatingId { get; set; }
    }
}
