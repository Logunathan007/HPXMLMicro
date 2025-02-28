
using EnergyScore.Domain.Entityies.AddressModels;

namespace EnergyScore.Domain.Entityies.HVACPlantModels
{
    public class HeatPump
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
        public Building Building { get; set; }
        public HVACPlant HVACPlant { get; set; }
        public Guid HVACPlantId { get; set; }
    }
}
