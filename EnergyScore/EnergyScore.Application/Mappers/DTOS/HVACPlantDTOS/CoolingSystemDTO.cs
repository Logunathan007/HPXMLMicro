
namespace EnergyScore.Application.Mappers.DTOS.HVACPlantDTOS
{
    public class CoolingSystemDTO
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
        public Guid DistributionSystemId { get; set; }
        public Guid BuildingId { get; set; }
    }
}
