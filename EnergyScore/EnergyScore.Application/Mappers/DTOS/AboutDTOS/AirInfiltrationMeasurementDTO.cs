namespace EnergyScore.Application.Mappers.DTOS.AboutDTOS
{
    public class AirInfiltrationMeasurementDTO
    {
        public Guid Id { get; set; }
        public double HousePressure { get; set; }
        public string UnitofMeasure { get; set; }
        public double AirLeakage { get; set; }
        public string LeakinessDescription { get; set; }
    }
}
