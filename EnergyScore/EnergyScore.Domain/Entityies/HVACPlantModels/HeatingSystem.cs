

using EnergyScore.Domain.Entityies.AddressModels;
using EnergyScore.Domain.Entityies.DistributionSystemModels;

namespace EnergyScore.Domain.Entityies.HVACPlantModels
{
    public class HeatingSystem
    {
        public Guid Id { get; set; }
        public string HeatingSystemName { get; set; }
        public double FractionHeatLoadServed { get; set; }
        public double HeatingCapacity { get; set; }
        public string HeatingSystemFuel { get; set; }
        public string HeatingSystemType { get; set; }
        public string AnnualHeatingEfficiencyUnits { get; set; }
        public double AnnualHeatingEfficiencyValue { get; set; }
        public int ModelYear { get; set; }
        public int YearInstalled { get; set; }
        public double FloorAreaServed { get; set; }
        public DistributionSystem DistributionSystem { get; set; }   
        public Guid DistributionSystemId { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
