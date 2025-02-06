
namespace EnergyScore.Application.Mappers.DTOS
{
    public class AirInfiltrationMeasurementDTO
    {
        public Guid Id { get; set; }
        public double HousePressure { get; set; }
        public string UnitofMeasure { get; set; }
        public float AirLeakage { get; set; }
        public string LeakinessDescription { get; set; }
    }
}
