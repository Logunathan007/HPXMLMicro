


namespace EnergyScore.Application.Mappers.DTOS.HVACPlantDTOS
{
    public class HeatPumpDTO
    {
        public Guid Id { get; set; }
        public string HeatPumpName { get; set; }
        public double FractionHeatLoadServed { get; set; }
        public double HeatingCapacity17F { get; set; }
        public double HeatingCapacity { get; set; }
        public double FractionCoolLoadServed { get; set; }
        public double CoolingCapacity { get; set; }
        public string HeatPumpType { get; set; }
        public string AnnualHeatingEfficiencyUnits { get; set; }
        public double AnnualHeatingEfficiencyValue { get; set; }
        public string AnnualCoolingEfficiencyUnits { get; set; }
        public double AnnualCoolingEfficiencyValue { get; set; }
        public int ModelYear { get; set; }
        public int YearInstalled { get; set; }
        public double FloorAreaServed { get; set; }
        public Guid BuildingId { get; set; }
    }
}
