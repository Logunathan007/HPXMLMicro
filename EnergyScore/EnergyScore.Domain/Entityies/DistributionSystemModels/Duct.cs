using EnergyScore.Domain.Entityies.CommonModels;
namespace EnergyScore.Domain.Entityies.DistributionSystem
{
    public class Duct
    {
        public Guid Id { get; set; }
        public double DuctInsulationRValue { get; set; }
        public double DuctInsulationThickness { get; set; }
        public string DuctInsulationMaterial { get; set; }
        public InsulationMaterialDynamicOptions DuctInsulationMaterialDynamicOptions { get; set; }
        public string DuctLocation { get; set; }
        public double FractionDuctArea { get; set; }
        public Guid DistributionSystemId { get; set; }
        public DistributionSystem DistributionSystem { get; set; }
    }
}
