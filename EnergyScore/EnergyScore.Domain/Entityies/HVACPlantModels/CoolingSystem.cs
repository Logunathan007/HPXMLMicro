
using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.DistributionSystemModels;

namespace EnergyScore.Domain.Entityies.HVACPlantModels
{
    public class CoolingSystem
    {
        public Guid Id { get; set; }
        public string CoolingSystemName { get; set; }
        public double FractionCoolLoadServed { get; set; }
        public double CoolingCapacity { get; set; }
        public string CoolingSystemType { get; set; }
        public string AnnualCoolingEfficiencyUnits { get; set; }
        public double AnnualCoolingEfficiencyValue { get; set; }
        public int ModelYear { get; set; }
        public int YearInstalled { get; set; }
        public double FloorAreaServed { get; set; }
        public DistributionSystem DistributionSystem { get; set; }
        public Guid DistributionSystemId { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
