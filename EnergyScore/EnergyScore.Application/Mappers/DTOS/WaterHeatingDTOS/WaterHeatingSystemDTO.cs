

namespace EnergyScore.Application.Mappers.DTOS.WaterHeating
{
    public class WaterHeatingSystemDTO
    {
        public Guid Id { get; set; }
        public string HeatingSystemName { get; set; }
        public double FractionDHWLoadServed { get; set; }
        public string WaterHeaterType { get; set; }
        public string FuelType { get; set; }
        public double EnergyFactor { get; set; }
        public double UniformEnergyFactor { get; set; }
        public Guid BuildingId { get; set; }
        public Guid WaterHeatingSystemsId { get; set; }
    }
}
